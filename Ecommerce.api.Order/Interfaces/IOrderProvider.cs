using Ecommerce.api.Order.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.api.order.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool isSuccess, IEnumerable<OrderDTO>,string ErrorMessage)> GetorderAsync();
        Task<(bool isSuccess, OrderDTO, string ErrorMessage)> GetorderByIdAsync(int Id);
    }
}
