 using Ecommerce.api.customer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.api.customer.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool isSuccess, IEnumerable<CustomerDTO>,string ErrorMessage)> GetCustomerAsync();
        Task<(bool isSuccess, CustomerDTO, string ErrorMessage)> GetCustomerByIdAsync(int Id);
    }
}
