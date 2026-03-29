using DataAccessLayer.Common;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.Setup
{
	public interface ICommonSetups
	{
		Task<ServiceResponse<object>> AddCodeGenerator(string TypeName);
		Task<string> ConvertIFormFileToBase64Async(IFormFile file);
		string GenerateSaleId();
		string GenerateShiftNumber();
		Task<string> GetCodeGenerator(string TypeName);
		string GetHostUrl();
		Task<ServiceResponse<object>> SaveImage(string base64Image, string type, string imageName);
		string SentenceCase(string input);
	}
}