using Ecommerce.api.product.Db;
using Ecommerce.api.product.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.api.product.Interfaces
{
    public interface IprodcutProvider
    {
        Task<(bool isSuccess, IEnumerable<ProductViewModel> productViewModels, string ErrorMessage)> GetProductAsync();
        Task<(bool isSuccess, ProductViewModel productViewModel, string ErrorMessage)> GetProductByIdAsync(int Id);
    }
}
