
using AutoMapper;
using Ecommerce.api.customer.Db;
using Ecommerce.api.customer.Interfaces;
using Ecommerce.api.customer.Model;
 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.api.customer.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly ILogger<CustomerProvider> _logge;

        private readonly IMapper _mapper;
        public CustomerProvider(CustomerDbContext customerDbContext,ILogger<CustomerProvider> logger, IMapper mapper)
        {
            _customerDbContext= customerDbContext;
            _logge= logger;
            _mapper= mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_customerDbContext.customers.Any())
            {
                for (int i = 1; i < 5; i++)
                {
                    _customerDbContext.customers.Add(new Db.Customer {Id=i, Address = "matrya"+i, Name = "zizy"+i+"kamel" });

                }
                _customerDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<CustomerDTO>, string ErrorMessage)> GetCustomerAsync()
        {
            try
            {
                IEnumerable<CustomerDTO> resultcustomers = null;
                string ErrorMessage = null;

                var customers = await _customerDbContext.customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    resultcustomers=  _mapper.Map<IEnumerable<CustomerDTO>>(customers);
                    return (true, resultcustomers, ErrorMessage);

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

        public async Task<(bool isSuccess, CustomerDTO, string ErrorMessage)> GetCustomerByIdAsync(int Id)
        {
            try
            {
                CustomerDTO resultcustomer = null;
                string ErrorMessage = null;

                var customer = await _customerDbContext.customers.Where(ent=>ent.Id==Id).FirstOrDefaultAsync();
                if (customer != null)
                {
                    resultcustomer = _mapper.Map<CustomerDTO>(customer);
                    return (true, resultcustomer, ErrorMessage);

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
