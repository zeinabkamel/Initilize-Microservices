using Ecommerce.api.Search.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.api.Search.Interfaces
{
   
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<ProductViewModel> Products, string ErrorMessage)> GetProductsAsync();
    }
}

