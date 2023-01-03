using Ecommerce.api.Search.Interfaces;
using Ecommerce.api.Search.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.api.Search
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var test = new Uri(Configuration["Services:Orders"]);
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductsService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddHttpClient("CustomerService", config => {
                config.BaseAddress = new Uri(Configuration["Services:Customers"]);
            }).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(5)));

            services.AddHttpClient("ProductsService", config => {
                config.BaseAddress = new Uri(Configuration["Services:Products"]);
            }).AddTransientHttpErrorPolicy(policy=> policy.WaitAndRetryAsync(2,_ => TimeSpan.FromSeconds(5)));
            services.AddHttpClient("ServiceOrder", config => {
                config.BaseAddress = new Uri(Configuration["Services:Orders"]);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
