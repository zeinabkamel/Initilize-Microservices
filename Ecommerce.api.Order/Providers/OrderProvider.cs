
using AutoMapper;
 using Ecommerce.api.order.Interfaces;
using Ecommerce.api.Order.Db;
using Ecommerce.api.Order.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.api.order.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly ILogger<OrderProvider> _logge;

        private readonly IMapper _mapper;
        public OrderProvider(OrderDbContext orderDbContext,ILogger<OrderProvider> logger, IMapper mapper)
        {
            _orderDbContext= orderDbContext;
            _logge= logger;
            _mapper= mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_orderDbContext.orders.Any())
            {
                _orderDbContext.orders.Add(new Order.Db.Order()
                {
                    Id = 1,
                    CutomerId = 1,
                    OrderDate = DateTime.Now,
                    orderItems = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _orderDbContext.orders.Add(new Order.Db.Order()
                {
                    Id = 2,
                    CutomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    orderItems = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _orderDbContext.orders.Add(new Order.Db.Order()
                {
                    Id = 3,
                    CutomerId = 2,
                    OrderDate = DateTime.Now,
                    orderItems = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                 _orderDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<OrderDTO>, string ErrorMessage)> GetorderAsync()
        {
            try
            {
                IEnumerable<OrderDTO> resultorders = null;
                string ErrorMessage = null;

                var orders = await _orderDbContext.orders.ToListAsync();
                if (orders != null && orders.Any())
                {
                    resultorders=  _mapper.Map<IEnumerable<OrderDTO>>(orders);
                    return (true, resultorders, ErrorMessage);

                }
                else
                {
                    ErrorMessage = "Not Found";
                    return (false, null, ErrorMessage);

                }

            }
            catch (Exception ex)
            {
                _logge.LogError(ex.Message, ex);
                return (false, null, ex.Message);

            }

        }

        public async Task<(bool isSuccess, OrderDTO, string ErrorMessage)> GetorderByIdAsync(int Id)
        {
            try
            {
                OrderDTO resultorder = null;
                string ErrorMessage = null;

                var order = await _orderDbContext.orders.Where(ent=>ent.CutomerId==Id).FirstOrDefaultAsync();
                if (order != null)
                {
                    resultorder = _mapper.Map<OrderDTO>(order);
                    return (true, resultorder, ErrorMessage);

                }
                else
                {
                    ErrorMessage = "Not Found";
                    return (false, null, ErrorMessage);

                }

            }
            catch (Exception ex)
            {
                _logge.LogError(ex.Message, ex);
                return (false, null, ex.Message);

            }

        }

    }
}
