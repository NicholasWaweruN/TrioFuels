using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.CustomerService;
using BusinessLogic.SetupService;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Customer;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Customers.Complains
{
	// This class handles customer complaints management
	public class Complain
	{
		// Injected dependencies
		private readonly OTOContext _context; // Database context for accessing entities
		private readonly IAuthCommonTasks _authentication; // Authentication tasks for user-related information
		private readonly ILogger<Complain> _logger; // Logger for logging information and errors

		// Constructor for dependency injection
		public Complain(OTOContext context, IAuthCommonTasks authentication, ILogger<Complain> logger)
		{
			_context = context;
			_authentication = authentication;
			_logger = logger;
		}

		// Method to add a new complaint
		public async Task<ServiceResponse<object>> AddComplain(AddComplainDto complainDTO)
		{
			try
			{
				// Check if the vehicle with the given VehicleCode exists
				var vehicleexisting = await _context.Vehicles.FirstOrDefaultAsync(x =>
					x.VehicleCode.Equals(complainDTO.VehicleCode));

				// If vehicle does not exist, return an informative response
				if (vehicleexisting == null)
					return ServiceResponse<object>.Information($"Vehicle with code {complainDTO.VehicleCode} does not exist", null);

				// Create a new Customer_Complains entity
				var complain = new Customer_Complains
				{
					ComplainCode = Guid.NewGuid().ToString(), // Generate a new unique ComplainCode
					VehicleCode = complainDTO.VehicleCode, // Assign the vehicle code
					ComplainDescription = complainDTO.ComplainDescription, // Assign the complaint description
					IsActive = true, // Set IsActive to true for new complaints
					DateCreated = DateTime.UtcNow, // Set the current date and time
					UserCode = _authentication.Usercode(), // Get the user code from the authentication service
					CustomerCode = vehicleexisting.CustomerCode // Assign the customer code from the existing vehicle
				};

				// Add the new complaint entity to the database context
				await _context.Customer_Complains.AddAsync(complain);

				// Save the changes to the database
				await _context.SaveChangesAsync();

				// Return a successful response with the created complaint entity
				return ServiceResponse<object>.Success("Complain added successfully", complain);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while adding complain");

				// Return an informative response in case of an exception
				return ServiceResponse<object>.Information("Complain not added", null);
			}
		}

		// Method to get all complaints
		public async Task<ServiceResponse<object>> GetAllComplains()
		{
			try
			{
				// Retrieve all complaints from the database
				var complains = await _context.Customer_Complains.ToListAsync();

				// Return a successful response with the list of complaints
				return ServiceResponse<object>.Success("Complains retrieved successfully", complains);
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				_logger.LogError(ex, "Error while retrieving complains");

				// Return an informative response in case of an exception
				return ServiceResponse<object>.Information("Complains not retrieved", null);
			}
		}

		// Method to change the status of a complaint
		public async Task<ServiceResponse<object>> ChangeComplainStatus(string complaincode)
		{
			try
			{
				// Retrieve the complaint entity based on the given complain code
				var complain = await _context.Customer_Complains.FirstOrDefaultAsync(x => x.ComplainCode == complaincode);

				// If the complaint is found, change its status
				if (complain is not null)
				{
					complain.IsActive = false; // Set IsActive to false (status change)

					// Update the complaint entity in the database context
					_context.Customer_Complains.Update(complain);

					// Save the changes to the database
					await _context.SaveChangesAsync();

					// Return a successful response with the updated complaint entity
					return ServiceResponse<object>.Success("Complain status changed successfully", complain);
				}
				else
				{
					// Return an informative response if the complaint was not found
					return ServiceResponse<object>.Information("Complain status not changed", null);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while changing complain status");

				// Return an informative response in case of an exception
				return ServiceResponse<object>.Information("Complain status not changed", null);
			}
		}
	}
}
