using System.Collections.Generic;
using System;

namespace Ecommerce.api.Search.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int CutomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> orderItems { get; set; }
    }
}
