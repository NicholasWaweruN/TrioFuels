using BusinessLogic.DashBoard;
using BussinessLogic.Sales.SalesData;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Syncfusion.XlsIO.Implementation.PivotAnalysis;

namespace BussinessLogic.DashBoard
{
	public class DashBoard : IDashBoard
    {
		/// <summary>
		/// 
		/// </summary>
        private readonly OTOContext _context;
		private readonly ISalesManagementService _salesService;
		public DashBoard(OTOContext context, ISalesManagementService salesService)

		{
            _context = context;
			_salesService = salesService;
        }

		//Total customers, total Vehicles,
		//Sold Quantities(this week, last week)
		//Total served vehicles today.
		//payments pie chart
		//Sales (today, 7 days) (Quantity per dateranget
		//top performing stations(5) (this week, last week

		public class DashboardDataDto
		{
			public int TotalCustomers { get; set; }
			public int TotalVehicles { get; set; }
			public decimal ThisWeekSoldQuantities { get; set; }
			public decimal LastWeekSoldQuantities { get; set; }
			public int TotalServedVehiclesToday { get; set; }
			public decimal SalesToday { get; set; }
			public decimal Sales7Days { get; set; }
			public decimal SalesThisMonth { get; set; }
			public decimal Petrol { get; set; }
			public decimal Diesel { get; set; }
			public decimal Lpg { get; set; }
			public List<TopPerformingStationDto> TopPerformingStations { get; set; } = new List<TopPerformingStationDto>();
			public SalesPagedResult Sales { get; set; } = new SalesPagedResult(); // Add this for live transactions
		}


		public class SalesPageResult
		{
			public int TotalRecords { get; set; }
			public int PageNumber { get; set; }
			public int PageSize { get; set; }
		}

		public class SalesPagedResult
		{
			public int TotalRecords { get; set; }
			public int PageNumber { get; set; }
			public int PageSize { get; set; }
			public List<SaleTransactionDto> Sales { get; set; } = new List<SaleTransactionDto>();
		}
		public class SaleTransactionDto
		{
			public string StationName { get; set; } = string.Empty;
			public string NozzleCode { get; set; } = string.Empty;
			public decimal Quantity { get; set; }
			public string VehicleRegistrationNumber { get; set; } = string.Empty;
			public string DispenserName { get; set; } = string.Empty;
			public string NozzleName { get; set; } = string.Empty;
			public string PaymentTypeName { get; set; } = string.Empty;
			public string SaleId { get; set; } = string.Empty;
			public DateTime DateCreated { get; set; }
			public string ShiftNumber { get; set; } = string.Empty;
			public string DispenserCode { get; set; } = string.Empty;
			public string StationCode { get; set; } = string.Empty;
			public string PetroleumName { get; set; } = string.Empty;
			public decimal Amount { get; set; }
		}


		public class TopPerformingStationDto
        {
            public string StationName { get; set; } = string.Empty;
             [Precision(18,2)] public decimal QuantitySold { get; set; }
        }


		public async Task<ServiceResponse<object>> GetDashBoardData()
		{
			var today = DateTime.UtcNow.Date;
			var thisWeek = today.AddDays(-7);
			var lastWeek = today.AddDays(-14);

			var quantitiesQuery = _context.QuantityTransactions.Where(x => x.DateCreated.Date >= lastWeek.Date);

			var quantitiesDiesel = await (from q in _context.QuantityTransactions
										  join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
										  join pp in _context.PetroleumProducts on d.PetroleumCode equals pp.PetroleumCode
										  where pp.PetroleumName == "Diesel" && q.DateCreated.Date == today
										  select q).SumAsync(x => x.QuantityCredit - x.QuantityDebit);

			var quantitiesPetrol = await (from q in _context.QuantityTransactions
										  join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
										  join pp in _context.PetroleumProducts on d.PetroleumCode equals pp.PetroleumCode
										  where pp.PetroleumName == "Petrol" && q.DateCreated.Date == today
										  select q).SumAsync(x => x.QuantityCredit - x.QuantityDebit);

			var quantitiesLpg = await (from q in _context.QuantityTransactions
									   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
									   join pp in _context.PetroleumProducts on d.PetroleumCode equals pp.PetroleumCode
									   where pp.PetroleumName == "Autogas" && q.DateCreated.Date == today
									   select q).SumAsync(x => x.QuantityCredit - x.QuantityDebit);

			var depletedCredit = await (
											from ct in _context.CreditTransactions
											join c in _context.Customers
												on ct.CustomerCode equals c.CustomerCode
											group ct by new
											{
												c.CustomerCode,
												c.CustomerName,
												c.CreditLimit
											} into g
											select new
											{
												g.Key.CustomerCode,
												g.Key.CustomerName,
												g.Key.CreditLimit,
												CreditBalance = g.Sum(x => x.Credit-x.Debit) 
											}
										).ToListAsync();

			// Get live feed sales (last 10 transactions)
			var liveFeedSalesResponse = await _salesService.AllSales(
				stationCode: null,
				shiftNumber: null,
				dispenserName: null,
				nozzleName: null,
				paymentTypeName: null,
				startDate: null,
				endDate: null,
				pageNumber: 1,
				pageSize: 10,
				orderByColumn: "DateCreated",
				isDescending: true
			);

			var dashboardData = await quantitiesQuery
				.GroupBy(x => 1)
				.Select(g => new DashboardDataDto
				{
					TotalCustomers = _context.Customers.Count(),
					TotalVehicles = _context.Vehicles.Count(),
					ThisWeekSoldQuantities = g.Where(x => x.DateCreated.Date >= thisWeek).Sum(x => x.QuantityCredit - x.QuantityDebit),
					LastWeekSoldQuantities = g.Where(x => x.DateCreated.Date >= lastWeek && x.DateCreated.Date < thisWeek).Sum(x => x.QuantityCredit - x.QuantityDebit),
					TotalServedVehiclesToday = g.Where(x => x.DateCreated.Date == today).Count(),
					SalesToday = g.Where(x => x.DateCreated.Date == today).Sum(x => x.QuantityCredit - x.QuantityDebit),
					Sales7Days = g.Where(x => x.DateCreated.Date >= thisWeek).Sum(x => x.QuantityCredit - x.QuantityDebit),
					SalesThisMonth = g.Where(x => x.DateCreated.Month == today.Month).Sum(x => x.QuantityCredit - x.QuantityDebit),
					Petrol = quantitiesPetrol,
					Diesel = quantitiesDiesel,
					Lpg = quantitiesLpg,
				}).FirstOrDefaultAsync();

			var topPerformingStations = await (from q in _context.QuantityTransactions
											   join s in _context.Stations on q.StationCode equals s.StationCode
											   where q.DateCreated.Date >= thisWeek
											   group q by new { q.StationCode, s.StationName } into g
											   select new TopPerformingStationDto
											   {
												   StationName = g.Key.StationName,
												   QuantitySold = g.Sum(x => x.QuantityCredit - x.QuantityDebit)
											   }).OrderByDescending(x => x.QuantitySold).Take(5).ToListAsync();

			if (dashboardData is not null)
			{
				dashboardData.TopPerformingStations = topPerformingStations;

				// Extract just the sales list for the frontend
				var result = new
				{
					dashboardData.TotalCustomers,
					dashboardData.TotalVehicles,
					dashboardData.ThisWeekSoldQuantities,
					dashboardData.LastWeekSoldQuantities,
					dashboardData.TotalServedVehiclesToday,
					dashboardData.SalesToday,
					dashboardData.Sales7Days,
					dashboardData.SalesThisMonth,
					dashboardData.Petrol,
					dashboardData.Diesel,
					dashboardData.Lpg,
					dashboardData.TopPerformingStations,
					sales = liveFeedSalesResponse.ResponseObject, // Use .Data, not .ResponseObject
					depletedCredit
				};


				return ServiceResponse<object>.Success("Data Found", result);
			}

			return ServiceResponse<object>.Information("No data found", new DashboardDataDto());
		}
	}
}
