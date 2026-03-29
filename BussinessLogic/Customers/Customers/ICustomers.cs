using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Customer;
using DataAccessLayer.EntityModels.Customer;

namespace BusinessLogic.CustomerService
{
	public interface ICustomers
	{
		static abstract ServiceResponse<object> OrganisationTypes();
		Task<ServiceResponse<object>> AddCustomer(CustomerDTO customerDTO);
		Task<ServiceResponse<object>> CustomerCreditLimit(UpdateCreditLimitDTO limit);
		Task<ServiceResponse<object>> CustomerDiscount(UpdateDiscount discount);
		Task<ServiceResponse<byte[]>> ExportAllCustomers();
		Task<ServiceResponse<object>> GetAllCustomers(string? customerName = null, string? customerPhone = null, int pageNumber = 1, int pageSize = 10);
		Task<ServiceResponse<object>> GetAllCustomers(string searchTerm);
		Task<Customer> GetCustomer(string customerCode);
		Task<ServiceResponse<object>> GetCustomerVehicles(string customerCode);
		Task<ServiceResponse<object>> OrganisationList();
		Task<ServiceResponse<object>> Organisations(RegisterOrganisationDTO register);
		Task<ServiceResponse<object>> UpdateCustomer(Customers.UpdateCustomerDTO updateCustomer, string customerCode);
		Task<ServiceResponse<object>> UpdateCustomerCreditLimit(UpdateCustomerCreditLimitDTO updateCustomer);
	}
}