using AutoMapper;
using Ecommerce.api.customer.Db;
using Ecommerce.api.customer.Model;

namespace Ecommerce.api.customer.ProductProfile
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ecommerce.api.customer.Db.Customer, CustomerDTO>().ReverseMap();
        }

    }
}
