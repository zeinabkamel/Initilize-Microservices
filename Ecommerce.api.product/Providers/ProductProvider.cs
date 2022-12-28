using AutoMapper;
using Ecommerce.api.product.Db;
using Ecommerce.api.product.Interfaces;
using Ecommerce.api.product.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.api.product.Providers
{
    public class ProductProvider : IprodcutProvider
    {
        private readonly ProductDbContext _productDbContext;
        private readonly ILogger<ProductProvider> _logge;

        private readonly IMapper _mapper;
        public ProductProvider(ProductDbContext productDbContext,ILogger<ProductProvider> logger, IMapper mapper)
        {
            _productDbContext= productDbContext;
            _logge= logger;
            _mapper= mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_productDbContext.products.Any())
            {
                for (int i = 1; i < 5; i++)
                {
                    _productDbContext.products.Add(new Product {Id=i, Code = "Ac"+i, Name = "Ac"+i+"name", Price = 2 * i * 10, Inventory = i });

                }
                _productDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<ProductViewModel>, string ErrorMessage)> GetProductAsync()
        {
            try
            {
                IEnumerable<ProductViewModel> resultProducts = null;
                string ErrorMessage = null;

                var products = await _productDbContext.products.ToListAsync();
                if (products != null && products.Any())
                {
                    resultProducts=  _mapper.Map<IEnumerable<ProductViewModel>>(products);
                    return (true, resultProducts, ErrorMessage);

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

        public async Task<(bool isSuccess, ProductViewModel, string ErrorMessage)> GetProductByIdAsync(int Id)
        {
            try
            {
               ProductViewModel resultProduct = null;
                string ErrorMessage = null;

                var product = await _productDbContext.products.Where(ent=>ent.Id==Id).FirstOrDefaultAsync();
                if (product != null)
                {
                    resultProduct = _mapper.Map<ProductViewModel>(product);
                    return (true, resultProduct, ErrorMessage);

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
