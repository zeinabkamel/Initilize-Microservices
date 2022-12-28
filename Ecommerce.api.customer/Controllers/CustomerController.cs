using Ecommerce.api.customer.Interfaces;
 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Ecommerce.api.Customer.Controllers
{
    [ApiController]
    [Route("api/Customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider _CustomerProvider;
        private readonly ILogger<CustomerController> _Logger;
        public CustomerController(ICustomerProvider CustomerProvider, ILogger<CustomerController> Logger)
        {
            _CustomerProvider = CustomerProvider;
            _Logger = Logger;

        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var Res = await _CustomerProvider.GetCustomerAsync();
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
        public async Task<IActionResult> GetCustomer(int id)
        {
            var Res = await _CustomerProvider.GetCustomerByIdAsync(id);
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
