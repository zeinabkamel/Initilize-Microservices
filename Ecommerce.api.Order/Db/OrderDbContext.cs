using Microsoft.EntityFrameworkCore;

namespace Ecommerce.api.Order.Db
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public OrderDbContext(DbContextOptions options) :base(options)
        {
        }
    }
}
