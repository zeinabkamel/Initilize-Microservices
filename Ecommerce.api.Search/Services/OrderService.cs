using Ecommerce.api.Search.Interfaces;
using Ecommerce.api.Search.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Targets;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
                using var client2 = new RestClient($"https://localhost:44358/api/orders/{CustomerId}");
                 var client = _httpClientFactory.CreateClient("ServiceOrder");
                var response = await client.GetAsync($"api/orders/{CustomerId}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonstring = JsonConvert.SerializeObject(response.Content.ReadAsStringAsync().Result);

                    var test = JsonConvert.DeserializeObject<dynamic>(jsonstring);
                    try
                    {
                        test = JsonConvert.DeserializeObject<List<Order>>(test);
                    }catch(Exception ex)
                    {
                        if(ex.Source.ToString()== "Newtonsoft.Json")
                         test = JsonConvert.DeserializeObject<Order>(test);

                    }

                    if (test is List<Order>)
                    {
                        return (true, test, null);


                    }
                    else if(test is Order)
                    {
                        var ret = new List<Order>();
                        ret.Add(test);
                        return (true, ret, null);

                    }
                    //var content = await response.Content.ReadAsStringAsync();
                    //var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    //var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    //return (true, result, null);

                  
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
