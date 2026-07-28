using DataAccessLayer.Common;
using DataAccessLayer.DTOs.PlateRecognition;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.PlateRecognitionService
{
	public interface IPlateRecognitionService
	{
		Task<ServiceResponse<PlateVerificationDto>> VerifyWalletVehicleAsync(
			IFormFile image, string customerCode, CancellationToken ct);
	}
}