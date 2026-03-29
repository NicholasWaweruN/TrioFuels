using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Sales.NewSales
{
	public class MovevhicleToAnotherProduct
	{
		private readonly OTOContext _context;

		public MovevhicleToAnotherProduct(OTOContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task MoveCompletedVehiclesAsync()
		{
			const decimal requiredExcess = 26400;
			const decimal excessRatePerLitre = 62;

			// Step 1: Get all vehicles under ProductCode '04'
			var eligibleVehicles = await _context.Vehicles
				.Where(v => v.ProductCode == "04")
				.Select(v => new
				{
					Vehicle = v,
					TotalLitres = _context.VwSalesData
						.Where(s => s.Vehicle == v.VehicleRegistrationNumber && s.ProductCode == "04" && s.HasValue == true)
						.Sum(s => (decimal?)s.Litres) ?? 0
				})
				.ToListAsync();

			// Step 2: Filter those who have paid off the installation cost
			var vehiclesToUpdate = eligibleVehicles
				.Where(v => v.TotalLitres * excessRatePerLitre >= requiredExcess)
				.Select(v => v.Vehicle)
				.ToList();

			if (vehiclesToUpdate.Count > 0)
			{
				var movedVehicleRegNumbers = new List<string>();

				foreach (var vehicle in vehiclesToUpdate)
				{
					vehicle.ProductCode = "05";
					movedVehicleRegNumbers.Add(vehicle.VehicleRegistrationNumber);
				}

				await _context.SaveChangesAsync();

				// Send email notification
				await SendVehicleMovedEmailAsync(movedVehicleRegNumbers);
			}
		}

		private static async Task SendVehicleMovedEmailAsync(List<string> vehicleRegNumbers)
		{
			var subject = "Vehicles Moved to New Price Category";
			var body = new StringBuilder();
			body.AppendLine("Hello,");
			body.AppendLine();
			body.AppendLine("The following vehicles have fully paid their installation cost (26,400 KES)" +
				" and have been moved to the new price category of 110:");
			body.AppendLine();

			foreach (var reg in vehicleRegNumbers)
			{
				body.AppendLine($"- {reg}");
			}

			body.AppendLine();
			body.AppendLine("Regards,");
			body.AppendLine("Fuel System");

			using var message = new MailMessage();
			message.From = new MailAddress("your_email@example.com", "Fuel System");
			message.To.Add("manager@example.com"); // Update to actual recipient(s)
			message.Subject = subject;
			message.Body = body.ToString();

			using var smtpClient = new SmtpClient("smtp.yourserver.com", 587)
			{
				Credentials = new System.Net.NetworkCredential("your_email@example.com", "your_app_password"),
				EnableSsl = true
			};

			await smtpClient.SendMailAsync(message);
		}
	}
}
