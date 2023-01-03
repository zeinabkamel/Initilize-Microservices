using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Ecommerce.api.product.Db;
using AutoMapper;
using Ecommerce.api.product.Providers;
using Ecommerce.api.product.ProductProfile;
using System.Linq;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync();
            Assert.IsTrue(product.isSuccess);
            Assert.IsTrue(product.productViewModels.Any());
            Assert.IsNull(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductByIdAsync(1);
            Assert.IsTrue(product.isSuccess);
            Assert.IsNotNull(product.productViewModel);
            Assert.IsTrue(product.productViewModel.Id == 1);
            Assert.IsNull(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInvalidId))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductByIdAsync(-1);
            Assert.IsFalse(product.isSuccess);
            Assert.IsNull(product.productViewModel);
            Assert.IsNotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (double)(decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }

}
