namespace Safaricom_Daraja.Mpesa
{
	public interface IMpesaStatements
	{
		Task<byte[]> ExportMpesaStatementAsync(string? tillNumber, DateOnly? from, DateOnly? to, CancellationToken ct = default);
		Task<MpesaStatements.PagedResult<MpesaStatements.MpesaStatementLineDto>> GetMpesaStatementAsync(string? tillNumber = null, DateOnly? from = null, DateOnly? to = null, int pageNumber = 1, int pageSize = 50, CancellationToken ct = default);
		Task<List<MpesaStatements.MpesaStatementLineDto>> GetMpesaStatementForExportAsync(string? tillNumber = null, DateOnly? from = null, DateOnly? to = null, CancellationToken ct = default);
	}
}