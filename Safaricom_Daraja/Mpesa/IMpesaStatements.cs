using Safaricom_Daraja.Mpesa;
using static Safaricom_Daraja.Mpesa.MpesaStatements;

namespace Safaricom_Daraja.Mpesa
{
	public interface IMpesaStatements
	{
		Task<List<MpesaStatementLineDto>> GetMpesaStatementAsync(
			string? tillNumber = null,
			DateOnly? from = null,
			DateOnly? to = null,
			CancellationToken ct = default);

		Task<byte[]> ExportMpesaStatementAsync(
			string? tillNumber,
			DateOnly? from,
			DateOnly? to,
			CancellationToken ct = default);
	}
}