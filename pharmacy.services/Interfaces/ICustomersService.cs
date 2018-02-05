using System;
using System.Threading.Tasks;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<CustomerPoco> GetCustomerByUsername(string username);
        Task<CustomerPoco> GetCustomer(Guid id);
        Task<CustomerPoco> RegisterCustomer(CustomerPoco customer);
        Task UpdateCustomerDetails(CustomerPoco customer);
        Task ActivateCustomer(Guid id, string mobileVerificationCode);
    }
}
