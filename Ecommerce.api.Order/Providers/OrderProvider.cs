
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
                for (int i = 1; i < 5; i++)
                {
                    _orderDbContext.orders.Add(new Order.Db.Order { CutomerId=i,Id=i,OrderDate=DateTime.Now,Total=10 });

                }
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
