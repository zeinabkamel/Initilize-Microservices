using Ecommerce.api.Search.Interfaces;
using Ecommerce.api.Search.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            _logger = logger;   
            _httpClientFactory = httpClientFactory; 

        }
        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int CustomerId)
        {
            try 
            {
                var client = _httpClientFactory.CreateClient("ServiceOrder");
                var response = await client.GetAsync($"api/orders/{CustomerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);


            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return (false, null, ex.Message);
            }

        }
    }
}
