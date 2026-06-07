using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace BussinessLogic.Personal_Wallet
{
	public class WalletDashBoard
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;

		public WalletDashBoard(OTOContext context, IAuthCommonTasks authentication)
		{
			_context = context;
			_authentication = authentication;
		}

		public async Task<ServiceResponse<object>> PersonalWalletDashBoard(string walletId)
		{
			try
			{
				var userCode = _authentication.Usercode();
				var userName = _authentication.Name();
				var currentMonth = DateTime.UtcNow.Month;

				// Retrieve only relevant transactions from the database
				var transactions = await _context.Wallet_Transactions_Personal
					.Where(p => p.WalletId == walletId)
					.Select(p => new { p.Credit, p.Debit, p.DateCreated })
					.ToListAsync();

				decimal totalTopUps = 0, totalSpend = 0, availableBalance = 0;
				decimal monthTopUps = 0, monthSpend = 0;

				foreach (var t in transactions)
				{
					totalTopUps += t.Credit;
					totalSpend += t.Debit;
					if (t.DateCreated.Month == currentMonth)
					{
						monthTopUps += t.Credit;
						monthSpend += t.Debit;
					}
				}
				availableBalance = totalTopUps - totalSpend;

				var quantityTransactions = await (
					from p in _context.Wallet_Transactions_Personal
					join x in _context.QuantityTransactions on p.SaleId equals x.SaleId
					join s in _context.Stations on x.StationCode equals s.StationCode
					join payment in _context.PaymentTransactions on p.SaleId equals payment.PaymentRefrence
					join vehicle in _context.Vehicles on p.VehicleCode equals vehicle.VehicleCode
					join u in _context.Users on p.UserCode equals u.UserCode into userGroup
					from users in userGroup.DefaultIfEmpty() // Left Join on Users
					where p.WalletId == walletId 
					select new
					{
						x.AmountCredit,
						x.AmountDebit,
						x.QuantityCredit,
						x.QuantityDebit,
						p.DateCreated,
						s.StationName,
						x.Price,
						x.SaleId,
						vehicle.VehicleRegistrationNumber,
						FueledBy = users != null ? string.Join(' ', new object[] { users.FirstName, users.MiddName, users.LastName }) : string.Empty, // Handle null case
						PaymentDetails = new PaymentDetail
						{
							TransAmount = payment.TransactionAmount - payment.TransactionAmountDebit,
							PaymentType = "Personal Wallet",
							TransID = payment.PaymentRefrence
						}
					}).ToListAsync();


				// Aggregate amount and quantity usage
				decimal monthLitres = 0, monthAmount = 0;
				foreach (var qt in quantityTransactions.Where(x => x.DateCreated.Month == currentMonth))
				{
					monthLitres += qt.QuantityCredit - qt.QuantityDebit;
					monthAmount += qt.AmountCredit - qt.AmountDebit;
				}

				// Get latest 10 transactions
				var latestTrans = quantityTransactions 
					.OrderByDescending(x => x.DateCreated)
					.Take(10)
					.Select(q => new LatestTransaction
					{
						TransAmount = q.AmountCredit,
						StationName = q.StationName,
						Quantity = q.QuantityCredit - q.QuantityDebit,
						DateFueled = q.DateCreated.ToString("yyyy-MM-dd HH:mm"),
						FueledBy = q.FueledBy,
						Price = q.Price,
						SaleId = q.SaleId,
						RegNo = q.VehicleRegistrationNumber,
						PaymentArray = [q.PaymentDetails]
					}).ToList();

				// Fetch wallet top-ups
				var walletTopUps = await _context.Wallet_Transactions_Personal
					.Where(w => w.WalletId == walletId)
					.Select(w => new WalletTopUpTransactions
					{
						DateCreated = w.DateCreated,
						Amount = w.Credit,
						PhoneNumber = w.PhoneNumber,
						ReferenceNo = w.TransactionCode,
					})
					.ToListAsync();

				var walletSummary = new WalletSummaryResponse
				{
					AvailableBalance = availableBalance,
					LatestTransactions = latestTrans,
					MonthAmount = monthAmount,
					MonthLitres = monthLitres,
					TotalSpend = totalSpend,
					TotalTopUp = totalTopUps,
					UserCode = userCode,
					UserName = userName,
					WalletTopUps = walletTopUps
				};

				return ServiceResponse<object>.Success("Wallet summary retrieved successfully", walletSummary);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while retrieving the wallet dashboard: {ex}", null);
			}
		}

		public async Task<ServiceResponse<object>> GetWalletQuantitySummaryAsync(string walletId)
		{
			try
			{
				var today = DateTime.Today;
				var startOfWeek = today.AddDays(-(int)today.DayOfWeek); // Sunday = start of the week
				var startOfMonth = new DateTime(today.Year, today.Month, 1);
				var startOfYear = new DateTime(today.Year, 1, 1);

				// Get only transactions for this wallet
				var quantityTransactions = await (
					from p in _context.Wallet_Transactions_Personal
					join x in _context.QuantityTransactions on p.SaleId equals x.SaleId
					where p.WalletId == walletId
					select new
					{
						x.QuantityCredit,
						x.QuantityDebit,
						x.AmountCredit,
						x.AmountDebit,
						p.DateCreated
					}).ToListAsync();

				decimal todayLitres = 0, todayAmount = 0;
				decimal weekLitres = 0, weekAmount = 0;
				decimal monthLitres = 0, monthAmount = 0;
				decimal yearLitres = 0, yearAmount = 0;

				foreach (var t in quantityTransactions)
				{
					var transDate = t.DateCreated.Date;
					var litres = t.QuantityCredit - t.QuantityDebit;
					var amount = t.AmountCredit - t.AmountDebit;

					if (transDate == today)
					{
						todayLitres += litres;
						todayAmount += amount;
					}
					if (transDate >= startOfWeek)
					{
						weekLitres += litres;
						weekAmount += amount;
					}
					if (transDate >= startOfMonth)
					{
						monthLitres += litres;
						monthAmount += amount;
					}
					if (transDate >= startOfYear)
					{
						yearLitres += litres;
						yearAmount += amount;
					}
				}

				var summary = new
				{
					Today = new { Litres = todayLitres, Amount = todayAmount },
					WeekToDate = new { Litres = weekLitres, Amount = weekAmount },
					MonthToDate = new { Litres = monthLitres, Amount = monthAmount },
					YearToDate = new { Litres = yearLitres, Amount = yearAmount }
				};

				return ServiceResponse<object>.Success("Wallet quantity summary retrieved successfully", summary);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An error occurred while retrieving quantity summary: " + ex.Message, null);
			}
		}


		public async Task<MemoryStream> ExportWalletTransactionsToExcel(string walletId, DateTime startDate, DateTime endDate)
		{

			var transactions = await _context.Wallet_Transactions_Personal
				.Where(w => w.WalletId == walletId && w.DateCreated.Date >= startDate.Date && w.DateCreated.Date <= endDate.Date)
				.OrderByDescending(w => w.DateCreated)
				.ToListAsync();

			var stream = new MemoryStream();

			using (var package = new ExcelPackage(stream))
			{
				var worksheet = package.Workbook.Worksheets.Add("Wallet Transactions");

				// Header row
				worksheet.Cells[1, 1].Value = "Date Created";
				worksheet.Cells[1, 2].Value = "Credit";
				worksheet.Cells[1, 3].Value = "Debit";
				worksheet.Cells[1, 4].Value = "Transaction Code";
				worksheet.Cells[1, 5].Value = "Sale ID";
				worksheet.Cells[1, 6].Value = "Phone Number";
				worksheet.Cells[1, 7].Value = "Description";

				using (var range = worksheet.Cells[1, 1, 1, 7])
				{
					range.Style.Font.Bold = true;
					range.Style.Fill.PatternType = ExcelFillStyle.Solid;
					range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
				}

				// Data rows
				int row = 2;
				foreach (var t in transactions)
				{
					worksheet.Cells[row, 1].Value = t.DateCreated.ToString("yyyy-MM-dd HH:mm");
					worksheet.Cells[row, 2].Value = t.Credit;
					worksheet.Cells[row, 3].Value = t.Debit;
					worksheet.Cells[row, 4].Value = t.TransactionCode;
					worksheet.Cells[row, 5].Value = t.SaleId;
					worksheet.Cells[row, 6].Value = t.PhoneNumber;
					worksheet.Cells[row, 7].Value = t.Description;
					row++;
				}

				worksheet.Cells.AutoFitColumns();
				await package.SaveAsync();
			}

			stream.Position = 0;
			return stream;
		}

		public async Task<ServiceResponse<object>> WalletTransactionsByDate(string walletId, DateTime startDate, DateTime endDate)
		{
			var transactions = await _context.Wallet_Transactions_Personal
				.Where(w => w.WalletId == walletId && w.DateCreated.Date >= startDate.Date && w.DateCreated.Date <= endDate.Date)
				.OrderByDescending(w => w.DateCreated)
				.ToListAsync();

			if (transactions.Count == 0)
			{
				return ServiceResponse<object>.Information("No transactions found for the specified date range", null);
			}
			var transactionList = transactions.Select(t => new
			{
				t.DateCreated,
				t.Credit,
				t.Debit,
				t.TransactionCode,
				t.SaleId,
				t.PhoneNumber,
				t.Description
			}).ToList();

			return ServiceResponse<object>.Success("Wallet transactions retrieved successfully", transactionList);


		}


		// Models
		public class WalletSummaryResponse
		{
			public string UserCode { get; set; } = string.Empty;
			public string UserName { get; set; } = string.Empty;
			public decimal MonthLitres { get; set; }
			public decimal MonthAmount { get; set; }
			public decimal TotalTopUp { get; set; }
			public decimal TotalSpend { get; set; }
			public decimal AvailableBalance { get; set; }
			public List<LatestTransaction> LatestTransactions { get; set; } = [];
			public List<WalletTopUpTransactions> WalletTopUps { get; set; } = [];
		}

		public class LatestTransaction
		{
			public string SaleId { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			public decimal Quantity { get; set; }
			public decimal TransAmount { get; set; }
			public decimal Price { get; set; }
			public string FueledBy { get; set; } = string.Empty;
			public string RegNo { get; set; } = string.Empty;
			public string DateFueled { get; set; } = string.Empty;
			public List<PaymentDetail> PaymentArray { get; set; } = [];
		}

		public class PaymentDetail
		{
			public string PaymentType { get; set; } = string.Empty;
			public string TransID { get; set; } = string.Empty;
			public decimal TransAmount { get; set; }
		}

		public class WalletTopUpTransactions
		{
			public decimal Amount { get; set; }
			public string ReferenceNo { get; set; } = string.Empty;
			public string PhoneNumber { get; set; } = string.Empty;
			public DateTime DateCreated { get; set; }
		}
	}
}
