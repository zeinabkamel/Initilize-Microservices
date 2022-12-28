using Microsoft.EntityFrameworkCore;

namespace Ecommerce.api.customer.Db
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> customers { get; set; }
        public CustomerDbContext(DbContextOptions options) :base(options)
        {
        }
    }
}
