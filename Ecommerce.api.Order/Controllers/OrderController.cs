using Ecommerce.api.order.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Ecommerce.api.product.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProvider _OrderProvider;
        private readonly ILogger<OrderController> _Logger;
        public OrderController(IOrderProvider OrderProvider, ILogger<OrderController> Logger)
        {
            _OrderProvider = OrderProvider;
            _Logger = Logger;

        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var Res = await _OrderProvider.GetorderAsync();
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
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrder(int customerId)
        {
            var Res = await _OrderProvider.GetorderByIdAsync(customerId);
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
