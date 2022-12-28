using Microsoft.EntityFrameworkCore;

namespace Ecommerce.api.product.Db
{
    public class ProductDbContext:DbContext
    {
        public DbSet<Product> products { get; set; }
        public ProductDbContext(DbContextOptions options) :base(options)
        {
        }
    }
}
