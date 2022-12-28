using Ecommerce.api.product.Interfaces;
using Ecommerce.api.product.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Ecommerce.api.product.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IprodcutProvider _ProductProvider;
        private readonly ILogger<ProductController> _Logger;
        public ProductController(IprodcutProvider ProductProvider, ILogger<ProductController> Logger)
        {
            _ProductProvider = ProductProvider;
            _Logger = Logger;

        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var Res = await _ProductProvider.GetProductAsync();
            if (Res.isSuccess)
            {
                _Logger.LogInformation("get the data   process is successfully done");

                return Ok(Res.Item2);
            }
            else
            {
                _Logger.LogError("Error while getting Data " + Res.ErrorMessage);
                 return NotFound();
            }
             
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var Res = await _ProductProvider.GetProductByIdAsync(id);
            if (Res.isSuccess)
            {
                _Logger.LogInformation("get the data   process is successfully done");

                return Ok(Res.Item2);
            }
            else
            {
                _Logger.LogError("Error while getting Data " + Res.ErrorMessage);
                return NotFound();
            }

        }
    }

   
}
