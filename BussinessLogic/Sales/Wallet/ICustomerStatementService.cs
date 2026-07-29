/*
 * ⚠️ ASSUMPTIONS TO VERIFY
 * - `_context` below is your existing DbContext (EF Core / Npgsql), injected
 *   the same way your other services already do it.
 * - Opening balance: since CustomerTransactions doesn't carry a running
 *   balance column, "opening balance" here means the net of every
 *   Credit-Debit transaction *before* FromDate. If you'd rather treat
 *   opening balance as always 0 for a statement window, delete the
 *   `openingBalanceQuery` block and set `openingBalance = 0`.
 * - PDF generation uses QuestPDF (https://www.questpdf.com/), a modern,
 *   actively maintained library with a free Community license for small
 *   businesses/individuals — install via `dotnet add package QuestPDF`.
 *   If you already have a PDF library in the FuelFlow stack, swap
 *   BuildStatementPdf's internals for that instead; the method signature
 *   (returns byte[]) can stay the same.
 * - Excel generation mirrors the ClosedXML pattern you already used for the
 *   vw_SalesData report.
 */
using DataAccessLayer.EntityModels.Personal_Wallet;

namespace BussinessLogic.Sales.Wallet
{
	public interface ICustomerStatementService
	{
		byte[] BuildStatementExcel(CustomerStatementDto statement);
		byte[] BuildStatementPdf(CustomerStatementDto statement);
		Task<CustomerStatementDto?> GetCustomerStatementAsync(string customerCode, DateTime fromDate, DateTime toDate);
	}
}