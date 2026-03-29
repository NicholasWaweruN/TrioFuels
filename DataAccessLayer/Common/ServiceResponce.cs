using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Common
{
    public class ServiceResponse
    {
        public int ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
    }


    public class ServiceResponse<T> : ServiceResponse
    {
        public T? ResponseObject { get; set; }

        public static ServiceResponse<T> Success(string message, T? obj = default)
        {
            return new ServiceResponse<T>
            {
                ResponseCode = Response.Success,
                ResponseMessage = message,
                ResponseObject = obj
            };
        }
	

		public static ServiceResponse<T> Information(string message, T? obj = default) => new()
		{

            ResponseCode = Response.Information,
            ResponseMessage = message.ToLower(),
            ResponseObject = obj
        };
        public static ServiceResponse Information(string message) => new()
		{
            ResponseCode = Response.Information,
            ResponseMessage = message
        };
        public static ServiceResponse<T> Error(string message, T? obj = default) => new()
		{
			
            ResponseCode = Response.Error,
            ResponseMessage = message,
            ResponseObject = obj

        };
  
		public static ServiceResponse<T> ChangePassWord(string message, T? obj = default) => new()
		{

			ResponseCode = Response.ChangePassWord,
			ResponseMessage = message.ToLower(),
			ResponseObject = obj
		};
		public static ServiceResponse<T> 
            InCustomer(string message, T? obj = default) => new()
		{
			ResponseCode = Response.WalkInCustomer,
			ResponseMessage = message.ToLower(),
			ResponseObject = obj
		};
		public static ServiceResponse<T> WalkInCustomer(string message, T? obj = default) => new()
		{

			ResponseCode = Response.WalkInCustomer,
			ResponseMessage = message.ToLower(),
			ResponseObject = obj
		};
	}
    public  class Response
    {
        public const int Success = 1;
        public const int Information = 2;
        public const int Error = 0;
		public const int ChangePassWord = 3;
		public const int WalkInCustomer = 4;
	}
  

    public static class Order
    {
        public const int Rejected = 0;
        public const int New_Order = 1;
        public const int Manager_Approved = 2;
        public const int Finanace_Approved = 3;
        public const int Assigned_Vehicle = 4;
        public const int Delivered = 5;
    }
    public static class Delivery
    {
        public const int Cancelled = 0;
        public const int New_Plan = 1;
        public const int Assigned_Orders = 2;
        public const int Inventoty_Loaded = 3;
        public const int Loaded = 4;
        public const int Completed = 5;
    }
    public static class ShiftStatus
    {
        public const int Open = 1;
        public const int Variance = 2;
        public const int Pending = 3;
        public const int Closed = 0;
        public const int Cancelled = 4;
    }
    public static class OpenType
    {
        public const int Open = 1;
        public const int Close = 0;
    }    
    public static class PaymetMethod
    {
        public const int Mpesa = 0;
        public const int Wallet = 1;
        public const int New_Conversions = 2;
        public const int Operational_Loss = 3;
        public const int Bank_Transfer = 4;
        public const int Employee_Mpesa_Payments = 5;
        public const int Insurance = 6;
        public const int Voucher = 7;
        public const int Calibration = 8;
        public const int Compesation_Fuel = 9;
		public const int BatchVoucher = 10;
		public const int Personal_Wallet = 11;
		public const int Cash = 12;
		public const int Credit = 13;
		public const int Loyalty = 14;
	} 
    public class Constants
    {
        public const string baseurl = "https://localhost:44300";
    }
    //get the current host url
    public class Apps
    {
        public const string BulkDashBoard = "01";
        public const string BulkApp = "02";
        public const string OtogasDashBoard = "03";
        public const string OtogasApp = "04";
    }
    public class AppType
    {
        public const string Bulk = "Bulk";
        public const string Otogas = "Otogas";
    }
	public class ProtoEmail
	{
		public const string Email = "@protoenergy.com";
	}
	public class VehicleStatus
    {
        [Comment("Vehicle is active")]
        public const string Active = "01";
        [Comment("Vehicle is inactive")]
        public const string Inactive = "02";
        [Comment("Vehicle is in the process of being converted") ]
        public const string Pending = "03";
        [Comment("Vehicle gas kit has been uninstalled from the vehicle")]
        public const string Uninstalled = "04";
    }
	public class Products
	{
		public const string Rental = "02";
		public const string OutRight = "01";
		public const string Walk_In = "03";
	}
	public enum CustomerStage
	{
		Suspect = 1,
		Lead = 2,
		QualifiedLead = 3,
		Prospect = 4,
		Customer = 5,
		LoyalCustomer = 6,
		Churned = 7
	}


}

