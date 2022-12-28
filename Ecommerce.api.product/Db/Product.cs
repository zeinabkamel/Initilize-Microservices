namespace Ecommerce.api.product.Db
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Inventory { get; set; }

        public string Code { get; set; }

    }
}
