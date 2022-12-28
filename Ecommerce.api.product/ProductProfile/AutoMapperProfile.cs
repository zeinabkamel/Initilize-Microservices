using AutoMapper;
using Ecommerce.api.product.Db;
using Ecommerce.api.product.Model;

namespace Ecommerce.api.product.ProductProfile
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }

    }
}
