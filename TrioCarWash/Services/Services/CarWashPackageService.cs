using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.CarWash;
using DataAccessLayer.EntityModels.Grleamify;
using Microsoft.EntityFrameworkCore;

namespace TrioCarWash.Services.Services;

public interface ICarWashPackageService
{
	Task<ServiceResponse<List<PackageDto>>> GetPackagesAsync(long vehicleTypeId);
	Task<ServiceResponse<PackageDto>> GetPackageByIdAsync(long packageId, long vehicleTypeId);
	Task<ServiceResponse<PackageDto>> CreatePackageAsync(CreatePackageDto dto);
	Task<ServiceResponse<PackageDto>> UpdatePackageAsync(UpdatePackageDto dto);
	Task<ServiceResponse<bool>> DeactivatePackageAsync(long packageId);

	// Internal — used by CarWashSalesService when expanding a package into
	// sale lines. Returns tracked entities with Items/VehicleTypePrices loaded.
	Task<Dictionary<long, CarWashPackage>> GetActivePackagesForSaleAsync(IEnumerable<long> packageIds);
}

public class CarWashPackageService : ICarWashPackageService
{
	private readonly OTOContext _db;

	public CarWashPackageService(OTOContext db)
	{
		_db = db;
	}

	public async Task<ServiceResponse<List<PackageDto>>> GetPackagesAsync(long vehicleTypeId)
	{
		var vehicleTypeExists = await _db.VehicleTypes.AnyAsync(v => v.Id == vehicleTypeId && v.IsActive);
		if (!vehicleTypeExists)
			return ServiceResponse<List<PackageDto>>.Error("Vehicle type not found or inactive");

		var raw = await _db.CarWashPackages
			.AsNoTracking()
			.Where(pkg => pkg.IsActive)
			.OrderBy(pkg => pkg.Name)
			.Select(pkg => new
			{
				pkg.Id,
				pkg.Name,
				BundlePrice = pkg.VehicleTypePrices
					.Where(vp => vp.VehicleTypeId == vehicleTypeId)
					.Select(vp => (decimal?)vp.Price)
					.FirstOrDefault(),
				Products = pkg.Items.Select(i => new
				{
					i.ProductId,
					i.Product.Name,
					Price = i.Product.VehicleTypePrices
						.Where(vp => vp.VehicleTypeId == vehicleTypeId)
						.Select(vp => (decimal?)vp.Price)
						.FirstOrDefault() ?? i.Product.Price
				}).ToList()
			})
			.ToListAsync();

		// Packages with no price configured for this vehicle type are excluded
		// from listings — a 0 price would look like a free bundle. This is a
		// listing-only rule; CreateSaleAsync treats the same condition as a
		// hard error instead, since by then someone's mid-transaction.
		var result = raw
			.Where(p => p.BundlePrice.HasValue)
			.Select(ToPackageDto)
			.ToList();

		return ServiceResponse<List<PackageDto>>.Success("OK", result);
	}

	public async Task<ServiceResponse<PackageDto>> GetPackageByIdAsync(long packageId, long vehicleTypeId)
	{
		var pkg = await _db.CarWashPackages
			.AsNoTracking()
			.Where(p => p.Id == packageId && p.IsActive)
			.Select(pkg => new
			{
				pkg.Id,
				pkg.Name,
				BundlePrice = pkg.VehicleTypePrices
					.Where(vp => vp.VehicleTypeId == vehicleTypeId)
					.Select(vp => (decimal?)vp.Price)
					.FirstOrDefault(),
				Products = pkg.Items.Select(i => new
				{
					i.ProductId,
					i.Product.Name,
					Price = i.Product.VehicleTypePrices
						.Where(vp => vp.VehicleTypeId == vehicleTypeId)
						.Select(vp => (decimal?)vp.Price)
						.FirstOrDefault() ?? i.Product.Price
				}).ToList()
			})
			.FirstOrDefaultAsync();

		if (pkg == null)
			return ServiceResponse<PackageDto>.Error("Package not found or inactive");

		if (!pkg.BundlePrice.HasValue)
			return ServiceResponse<PackageDto>.Error("This package has no price configured for the selected vehicle type");

		return ServiceResponse<PackageDto>.Success("OK", ToPackageDto(pkg));
	}

	public async Task<ServiceResponse<PackageDto>> CreatePackageAsync(CreatePackageDto dto)
	{
		var validationError = await ValidatePackageInputAsync(dto.Name, dto.ProductIds, dto.VehicleTypePrices);
		if (validationError != null)
			return ServiceResponse<PackageDto>.Error(validationError);

		var package = new CarWashPackage
		{
			Name = dto.Name.Trim(),
			Items = dto.ProductIds.Distinct()
				.Select(id => new CarWashPackageItem { ProductId = id })
				.ToList(),
			VehicleTypePrices = dto.VehicleTypePrices
				.Select(p => new CarWashPackagePrice { VehicleTypeId = p.VehicleTypeId, Price = p.Price })
				.ToList()
		};

		_db.CarWashPackages.Add(package);
		await _db.SaveChangesAsync();

		return ServiceResponse<PackageDto>.Success("Package created", new PackageDto
		{
			PackageId = package.Id,
			Name = package.Name
		});
	}

	public async Task<ServiceResponse<PackageDto>> UpdatePackageAsync(UpdatePackageDto dto)
	{
		var package = await _db.CarWashPackages
			.Include(p => p.Items)
			.Include(p => p.VehicleTypePrices)
			.FirstOrDefaultAsync(p => p.Id == dto.PackageId && p.IsActive);

		if (package == null)
			return ServiceResponse<PackageDto>.Error("Package not found or inactive");

		var validationError = await ValidatePackageInputAsync(dto.Name, dto.ProductIds, dto.VehicleTypePrices);
		if (validationError != null)
			return ServiceResponse<PackageDto>.Error(validationError);

		// Existing sale history references CarWashTransactionItem rows by
		// ProductId snapshot, not by CarWashPackageItem, so it's safe to
		// fully replace the composition/pricing here — past sales are
		// unaffected either way.
		package.Name = dto.Name.Trim();

		_db.CarWashPackageItems.RemoveRange(package.Items);
		package.Items = dto.ProductIds.Distinct()
			.Select(id => new CarWashPackageItem { PackageId = package.Id, ProductId = id })
			.ToList();

		_db.CarWashPackagePrices.RemoveRange(package.VehicleTypePrices);
		package.VehicleTypePrices = dto.VehicleTypePrices
			.Select(p => new CarWashPackagePrice { PackageId = package.Id, VehicleTypeId = p.VehicleTypeId, Price = p.Price })
			.ToList();

		await _db.SaveChangesAsync();

		return ServiceResponse<PackageDto>.Success("Package updated", new PackageDto
		{
			PackageId = package.Id,
			Name = package.Name
		});
	}

	public async Task<ServiceResponse<bool>> DeactivatePackageAsync(long packageId)
	{
		var package = await _db.CarWashPackages.FirstOrDefaultAsync(p => p.Id == packageId && p.IsActive);
		if (package == null)
			return ServiceResponse<bool>.Error("Package not found or already inactive");

		// Soft delete only — CarWashTransactionItem rows keep their PackageId,
		// so historical sales/receipts still resolve the package name correctly.
		package.IsActive = false;
		await _db.SaveChangesAsync();

		return ServiceResponse<bool>.Success("Package deactivated", true);
	}

	public async Task<Dictionary<long, CarWashPackage>> GetActivePackagesForSaleAsync(IEnumerable<long> packageIds)
	{
		var ids = packageIds.Distinct().ToList();

		return await _db.CarWashPackages
			.Include(p => p.Items)
			.Include(p => p.VehicleTypePrices)
			.Where(p => ids.Contains(p.Id) && p.IsActive)
			.ToDictionaryAsync(p => p.Id);
	}

	// -----------------------------------------------------------------------
	private async Task<string?> ValidatePackageInputAsync(
		string name, List<long> productIds, List<PackageVehicleTypePriceDto> vehicleTypePrices)
	{
		if (string.IsNullOrWhiteSpace(name))
			return "Package name is required";

		if (productIds == null || productIds.Distinct().Count() < 2)
			return "A package must bundle at least two distinct products";

		var distinctIds = productIds.Distinct().ToList();
		var validProductCount = await _db.CarWashProducts
			.CountAsync(p => distinctIds.Contains(p.Id) && p.IsActive);

		if (validProductCount != distinctIds.Count)
			return "One or more products were not found or inactive";

		if (vehicleTypePrices == null || vehicleTypePrices.Count == 0)
			return "At least one vehicle type price is required";

		if (vehicleTypePrices.Any(p => p.Price <= 0))
			return "Package price must be greater than zero";

		var vehicleTypeIds = vehicleTypePrices.Select(p => p.VehicleTypeId).ToList();
		if (vehicleTypeIds.Distinct().Count() != vehicleTypeIds.Count)
			return "Duplicate vehicle type prices were provided";

		var validVehicleTypeCount = await _db.VehicleTypes
			.CountAsync(v => vehicleTypeIds.Contains(v.Id) && v.IsActive);

		if (validVehicleTypeCount != vehicleTypeIds.Distinct().Count())
			return "One or more vehicle types were not found or inactive";

		return null;
	}

	private static PackageDto ToPackageDto<T>(T p) where T :
		// anonymous-type shape from the two query projections above
		notnull
	{
		dynamic pkg = p;
		return new PackageDto
		{
			PackageId = pkg.Id,
			Name = pkg.Name,
			Price = pkg.BundlePrice ?? 0,
			IndividualTotal = ((IEnumerable<dynamic>)pkg.Products).Sum(x => (decimal)x.Price),
			Products = ((IEnumerable<dynamic>)pkg.Products)
				.Select(x => new PackageProductDto { ProductId = x.ProductId, Name = x.Name })
				.ToList()
		};
	}
}