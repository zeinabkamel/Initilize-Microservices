using AutoMapper;
 

namespace Ecommerce.api.customer.ProductProfile
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ecommerce.api.Order.Db.Order, Ecommerce.api.Order.Model.OrderDTO>().ReverseMap();
            CreateMap<Ecommerce.api.Order.Db.OrderItem, Ecommerce.api.Order.Model.OrderItemDTO>().ReverseMap();

        }

    }
}
