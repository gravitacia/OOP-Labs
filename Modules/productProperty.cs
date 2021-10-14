namespace Shops.Modules
{
    public class ProductProperty
    {
        public ProductProperty(int price, int count)
        {
            Price = price;
            Count = count;
        }

        public int Count { get; set; }

        public int Price { get; set; }
    }
}