using Ecommerce.api.Search.Interfaces;
using Ecommerce.api.Search.Model;
using NLog.Targets;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductsService productsService;

        private readonly ICustomerService _customerService;

        public SearchService(IOrderService orderService, IProductsService productsService, ICustomerService customerService)
        {
            _orderService = orderService;
            this.productsService = productsService;
            this._customerService = customerService;
        }
        public async Task<(bool isSuccess, dynamic searchResult)> SearchAsync(int customerId)
        {
            var OrderResult = await _orderService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await _customerService.GetCustomerAsync(customerId);


            if (OrderResult.IsSuccess)
            {
                foreach (var orders in OrderResult.Orders)
                {
                    foreach (var item in orders.orderItems)
                    {
                        item.ProductName = productsResult.IsSuccess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product information is not available";
                    }
                }
                var result = new
                {
                    Customer = customersResult.IsSuccess ?
                                customersResult.Customer :
                                new { Name = "Customer information is not available" },
                    Orders = OrderResult.Orders
                };

                return (true, result);
            }
            return (false, null);
            
         }
         
    }
}
