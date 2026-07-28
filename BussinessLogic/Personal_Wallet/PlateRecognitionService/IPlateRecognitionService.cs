using DataAccessLayer.Common;
using DataAccessLayer.DTOs.PlateRecognition;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.PlateRecognitionService
{
	public interface IPlateRecognitionService
	{
		Task<ServiceResponse<PlateVerificationDto>> VerifyWalletVehicleAsync(
		   string base64Image, string customerCode, CancellationToken ct);
	}
}