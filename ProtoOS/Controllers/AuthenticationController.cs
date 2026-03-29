namespace ProtoOS.Controllers
{
	using BusinessLogic.Authentication.AddUsers;
	using BussinessLogic.Authentication.CommonTasks;
	using BusinessLogic.Authentication.UserApplications;
	using DataAccessLayer.DTOs.Authentication;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;
	using System.ComponentModel.DataAnnotations;
	using BussinessLogic.Authentication.SignIn;

	/// <summary>
	/// Defines the <see cref="AuthenticationController" />
	/// </summary>
	[Route("fuelflow/[controller]")]
	[ApiController]
	[Authorize]
	public class AuthenticationController : ControllerBase
	{
		/// <summary>
		/// Defines the _registerUsers
		/// </summary>
		private readonly IRegisterUsers _registerUsers;

		/// <summary>
		/// Defines the _signIn
		/// </summary>
		private readonly ISignInUser _signIn;

		/// <summary>
		/// Defines the _appsService
		/// </summary>
		private readonly IAppsService _appsService;

		/// <summary>
		/// Defines the _authtasks
		/// </summary>
		private readonly IAuthCommonTasks _authtasks;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticationController"/> class.
		/// </summary>
		/// <param name="registerUsers">The registerUsers<see cref="IRegisterUsers"/></param>
		/// <param name="signIn">The signIn<see cref="ISignInUser"/></param>
		/// <param name="appsService">The appsService<see cref="IAppsService"/></param>
		/// <param name="authtasks">The authtasks<see cref="IAuthCommonTasks"/></param>
		public AuthenticationController(IRegisterUsers registerUsers, ISignInUser signIn,
			IAppsService appsService, IAuthCommonTasks authtasks)
		{
			_registerUsers = registerUsers;
			_signIn = signIn;
			_appsService = appsService;
			_authtasks = authtasks;
		}

		/// <summary>
		/// The RegisterUser
		/// </summary>
		/// <param name="register">The register<see cref="RegisterModel"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("RegisterUser")]
		[Authorize(Roles = "can register a user")]
		public async Task<IActionResult> RegisterUser([FromBody] RegisterModel register)
		{
			var response = await _registerUsers.RegisterUserAsync(register);
			return Ok(response);
		}

		/// <summary>
		/// The SignInUser
		/// </summary>
		/// <param name="signIn">The signIn<see cref="EmailLoginModel"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("SignInUser")]
		[AllowAnonymous]
		public async Task<IActionResult> SignInUser([FromBody] EmailLoginModel signIn)
		{
			var response = await _signIn.SignInUserAsync(signIn);
			return Ok(response);
		}

		/// <summary>
		/// The ResetPassword
		/// </summary>
		/// <param name="password">The password<see cref="string"/></param>
		/// <param name="confirmPassword">The confirmPassword<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("ResetPassword")]
		[Authorize]
		public async Task<IActionResult> ResetPassword([FromBody] string password, string confirmPassword)
		{
			var response = await _signIn.ResetPasswordAsync(password, confirmPassword);
			return Ok(response);
		}

		/// <summary>
		/// The ConfirmPhoneNumber
		/// </summary>
		/// <param name="phoneNumber">The phoneNumber<see cref="string"/></param>
		/// <param name="otp">The otp<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("ConfirmPhoneNumber")]
		[Authorize]
		public async Task<IActionResult> ConfirmPhoneNumber([FromQuery] string phoneNumber, [FromQuery] string otp)
		{
			var response = await _registerUsers.ConfirmPhoneNumberWithOtpAsync(phoneNumber, otp);
			return Ok(response);
		}

		/// <summary>
		/// The GetAllUsers
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpGet]
		[Route("GetAllUsers")]
		[Authorize(Roles = "can view all users")]
		public async Task<IActionResult> GetAllUsers()
		{
			var response = await _registerUsers.GetAllUsersAsync();
			return Ok(response);
		}

		/// <summary>
		/// The DeactivateUser
		/// </summary>
		/// <param name="userCode">The userCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("DeactivateUser")]
		[Authorize(Roles = "can deactivate users")]
		public async Task<IActionResult> DeactivateUser([FromQuery] string userCode)
		{
			var response = await _registerUsers.DeactivateUserAsync(userCode);
			return Ok(response);
		}

		[HttpPost]
		[Route("opt-out")]
		public async Task<IActionResult>OptOut ([FromQuery] string userCode)
		{
			var response = await _registerUsers.DeactivateUserAsync(userCode);
			return Ok(response);
		}
		/// <summary>
		/// The ActivateUser
		/// </summary>
		/// <param name="userCode">The userCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("ActivateUser")]
		[Authorize(Roles = "can activate users")]
		public async Task<IActionResult> ActivateUser([FromQuery] string userCode)
		{
			var response = await _registerUsers.ActivateUserAsync(userCode);
			return Ok(response);
		}

		/// <summary>
		/// The SendOTP
		/// </summary>
		/// <param name="phoneNumber">The phoneNumber<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpGet]
		[Route("SendOTP")]
		[AllowAnonymous]
		public async Task<IActionResult> SendOTP([FromQuery] string phoneNumber)
		{
			var response = await _signIn.SendOTPAsync(phoneNumber);
			return Ok(response);
		}

		/// <summary>
		/// The ForgotPassword
		/// </summary>
		/// <param name="reset">The reset<see cref="ResetPasswordModel"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("ChangePassword")]
		[AllowAnonymous]
		public async Task<IActionResult> ForgotPassword(ResetPasswordModel reset)
		{
			var response = await _signIn.ForgotPassword(reset);
			return Ok(response);
		}

		/// <summary>
		/// The UpdateUserDetails
		/// </summary>
		/// <param name="userCode">The userCode<see cref="string"/></param>
		/// <param name="update">The update<see cref="UpdateUsers"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("UpdateUserDetails")]
		[Authorize(Roles = "can update user details")]
		public async Task<IActionResult> UpdateUserDetails([FromQuery] string userCode, [FromBody] UpdateUsers update)
		{
			var response = await _registerUsers.UpdateUserDetailsAsync(userCode, update);
			return Ok(response);
		}

		/// <summary>
		/// The CurrentUserDetails
		/// </summary>
		/// <param name="userCode">The userCode<see cref="string?"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpGet]
		[Route("CurrentUserDetails")]
		[Authorize]
		public async Task<IActionResult> CurrentUserDetails()
		{
			var response = await _authtasks.GetUserDetails();
			return Ok(response);
		}

		[HttpGet]
		[Route("AllUsersDetails")]
		[Authorize(Roles = "can view all users")]
		public async Task<IActionResult> AllUserDetails([FromQuery] string? userCode)
		{
			var response = await _authtasks.GetUserDetailsAsync(userCode);
			return Ok(response);
		}

		/// <summary>
		/// The AssignUserToApp
		/// </summary>
		/// <param name="userCode">The userCode<see cref="string"/></param>
		/// <param name="appCode">The appCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("AssignUserToApp")]
		[Authorize(Roles = "can assign user an app")]
		public async Task<IActionResult> AssignUserToApp([FromQuery] string userCode, [FromQuery] string appCode)
		{
			var response = await _appsService.AssignUserToAppAsync(userCode, appCode);
			return Ok(response);
		}

		/// <summary>
		/// The RemoveUserFromApp
		/// </summary>
		/// <param name="userCode">The userCode<see cref="string"/></param>
		/// <param name="appCode">The appCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpPost]
		[Route("RemoveUserFromApp")]
		[Authorize(Roles = "can remove user from an app")]
		public async Task<IActionResult> RemoveUserFromApp([FromQuery] string userCode, [FromQuery] string appCode)
		{
			var response = await _appsService.RemoveUserAppsAsync(userCode, appCode);
			return Ok(response);
		}

		/// <summary>
		/// The GetUserApps
		/// </summary>
		/// <param name="userCode">The userCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpGet]
		[Route("GetUserApps")]
		[Authorize]
		public async Task<IActionResult> GetUserApps([FromQuery] string userCode)
		{
			var response = await _appsService.GetUserAppsAsync(userCode);
			return Ok(response);
		}

		/// <summary>
		/// The GetAllApps
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		[HttpGet]
		[Route("GetAllApps")]
		[Authorize]
		public async Task<IActionResult> GetAllApps()
		{
			var response = await _appsService.GetApps();
			return Ok(response);
		}
	}
}
