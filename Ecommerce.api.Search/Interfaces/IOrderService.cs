using Ecommerce.api.Search.Model;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Ecommerce.api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int CustomerId);
    }
}
