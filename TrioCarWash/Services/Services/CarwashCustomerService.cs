using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Grleamify;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrioCarWash.Services.Services
{

		public interface ICarwashCustomerService
		{
			Task<CarwashCustomer> AddCustomerAsync(AddCarwashCustomerDto dto);
			Task<ServiceResponse<List<CarwashCustomerSearchResultDto>>> SearchByPhoneAsync(string phoneNumber);
			Task<CarwashCreditTransaction> AddCreditTransactionAsync(CreateCreditTransactionDto dto);
		}

		public class CarwashCustomerService : ICarwashCustomerService
		{
			private readonly OTOContext _context;

			public CarwashCustomerService(OTOContext context)
			{
				_context = context;
			}

			public async Task<CarwashCustomer> AddCustomerAsync(AddCarwashCustomerDto dto)
			{
				var exists = await (from c in _context.CarwashCustomers
									where c.PhoneNumber == dto.PhoneNumber
									select c).AnyAsync();

				if (exists)
					throw new InvalidOperationException("A customer with this phone number already exists.");

				var customer = new CarwashCustomer
				{
					Name = dto.Name,
					PhoneNumber = dto.PhoneNumber,
					IsCreditCustomer = dto.IsCreditCustomer,
					CreditLimit = dto.CreditLimit,
					IsDiscountCustomer = dto.IsDiscountCustomer,
					DiscountAmount = dto.DiscountAmount,
					CurrentBalance = 0
				};

				_context.CarwashCustomers.Add(customer);
				await _context.SaveChangesAsync();
				return customer;
			}

		public async Task<ServiceResponse<List<CarwashCustomerSearchResultDto>>> SearchByPhoneAsync(string phoneNumber)
		{
			var results = from c in _context.CarwashCustomers
						  where c.PhoneNumber.Contains(phoneNumber)
						  select new CarwashCustomerSearchResultDto
						  {
							  Id = c.Id,
							  Name = c.Name,
							  PhoneNumber = c.PhoneNumber,
							  IsCreditCustomer = c.IsCreditCustomer,
							  CreditLimit = c.CreditLimit,
							  CurrentBalance = c.CurrentBalance,
							  IsDiscountCustomer = c.IsDiscountCustomer,
							  DiscountAmount = c.DiscountAmount
						  };

			var result = await results.ToListAsync();

			if(result.Count > 0)
				return ServiceResponse<List<CarwashCustomerSearchResultDto>>.Success("Record found", result);
			else return ServiceResponse<List<CarwashCustomerSearchResultDto>>.Information("No record found", result);

		}

		public async Task<CarwashCreditTransaction> AddCreditTransactionAsync(CreateCreditTransactionDto dto)
			{
				var customer = await (from c in _context.CarwashCustomers
									  where c.Id == dto.CarwashCustomerId
									  select c).FirstOrDefaultAsync();

				if (customer is null)
					throw new InvalidOperationException("Customer not found.");

				if (!customer.IsCreditCustomer)
					throw new InvalidOperationException("Customer is not a credit customer.");

				decimal debit = 0, credit = 0;

				if (dto.Type == CreditTransactionType.Debit)
				{
					// Credit being extended - check against limit
					if (customer.CurrentBalance + dto.Amount > customer.CreditLimit)
						throw new InvalidOperationException("This would exceed the customer's credit limit.");

					debit = dto.Amount;
					customer.CurrentBalance += dto.Amount;
				}
				else
				{
					// Payment being made
					credit = dto.Amount;
					customer.CurrentBalance -= dto.Amount;
					if (customer.CurrentBalance < 0)
						customer.CurrentBalance = 0;
				}

				var transaction = new CarwashCreditTransaction
				{
					CarwashCustomerId = customer.Id,
					Debit = debit,
					Credit = credit,
					RunningBalance = customer.CurrentBalance,
					Description = dto.Description,
					SaleId = dto.SaleId
				};

				_context.CarwashCreditTransactions.Add(transaction);
				await _context.SaveChangesAsync();
				return transaction;
			}
		}
	}

	public class AddCarwashCustomerDto
	{
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public bool IsCreditCustomer { get; set; }
		public decimal CreditLimit { get; set; }
		public bool IsDiscountCustomer { get; set; }
		public decimal DiscountAmount { get; set; }
	}

	public class CarwashCustomerSearchResultDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public bool IsCreditCustomer { get; set; }
		public decimal CreditLimit { get; set; }
		public decimal CurrentBalance { get; set; }
		public bool IsDiscountCustomer { get; set; }
		public decimal DiscountAmount { get; set; }
	}

	public enum CreditTransactionType { Debit, Credit }

public class CreateCreditTransactionDto
{
	public int CarwashCustomerId { get; set; }
	public decimal Amount { get; set; }
	public CreditTransactionType Type { get; set; }
	public string? Description { get; set; }
	public int? SaleId { get; set; }
}

