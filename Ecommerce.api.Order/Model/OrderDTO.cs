using System;
using System.Collections.Generic;

namespace Ecommerce.api.Order.Model
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CutomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public  List<OrderItemDTO> orderItems { get; set; }

    }
}
