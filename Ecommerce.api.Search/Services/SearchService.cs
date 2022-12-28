using Ecommerce.api.Search.Interfaces;
using Ecommerce.api.Search.Model;
using NLog.Targets;
using System.Threading.Tasks;

namespace Ecommerce.api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;   
        public SearchService(IOrderService orderService)
        {
            _orderService=orderService; 
        }
        public async Task<(bool isSuccess, dynamic searchResult)> SearchAsync(int customerId)
        {
            var OrderResult = await _orderService.GetOrdersAsync(customerId);
            if (OrderResult.IsSuccess)
            {
                var res = new
                {
                    Orders = OrderResult.Orders
                };
                return (true, res);
            }
            return(false, null);
            
         }
    }
}
