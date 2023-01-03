using Ecommerce.api.Search.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.api.Search.Interfaces
{
   
    public interface ICustomerService
    {
        Task<(bool IsSuccess, IEnumerable<CustomerDTO> customerDTOs, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int id);

    }
}

