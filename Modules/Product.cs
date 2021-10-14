namespace Shops.Modules
{
    public class Product
    {
        public Product(string productName, int count)
        {
            ProductName = productName;
            Count = count;
        }

        public int Count { get; set; }

        public string ProductName { get; set; }
    }
}