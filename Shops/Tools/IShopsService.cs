using Shops.Modules;

namespace Shops.Tools
{
    public interface IShopsService
    {
        Shop AddShop(string name, string addres);
        Product AddProduct(string name);
        void ProductTransfer(Shop shop, string name, int price, int count);
        Shop FindShop(string productName);
        void BuyProduct(string productName, int count, int buyersMoney);
        void ChangeProductPrice(Shop shop, string name, int newPrice);
    }
}