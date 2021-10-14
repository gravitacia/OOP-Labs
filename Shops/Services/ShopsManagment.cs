using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Modules;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopsManagment : IShopsManagment
    {
        private readonly List<Shop> _shops = new List<Shop>();
        private readonly List<Product> _products = new List<Product>();
        private int _shopId = 1;
        private int _productId = 0;
        private Random rand = new Random();

        public Shop AddShop(string name, string addres)
        {
            int money = 0;
            var shop = new Shop(name, _shopId++, addres, money);
            _shops.Add(shop);
            return shop;
        }

        public Product AddProduct(string name)
        {
            var product = new Product(name, _productId++);
            _products.Add(product);
            return product;
        }

        public void ProductTransfer(Shop shop, string name, int price, int count)
        {
            ProductProperty productProperty = shop.PriceAndCount(price, count);
            shop.AddProductToShop(AddProduct(name), productProperty);
        }

        public Shop FindShop(string productName)
        {
            int minValue = (from curShop in _shops from curProduct in curShop.GetProductList() where curProduct.Key.ProductName == productName select curProduct.Value.Price).Prepend(int.MaxValue).Min();

            foreach (var curShop in from curShop in _shops from curProduct in curShop.GetProductList() where curProduct.Value.Price == minValue select curShop)
            {
                return curShop;
            }

            throw new Exception("Warning!");
        }

        public void BuyProduct(string productName, int count)
            {
                int money = rand.Next(500, 1000);
                var buyer = new Buyer(money);
                foreach (Shop curShop in _shops)
                {
                    foreach (var curProduct in curShop.GetProductList().Where(curProduct => curProduct.Key.ProductName == productName))
                    {
                        if (curProduct.Value.Count > count)
                        {
                            int canBuy = curProduct.Value.Price * count;
                            if (buyer.Money > canBuy)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    curShop.RemoveProductFromShop(curProduct.Key);
                                    buyer.Money -= curProduct.Value.Price;
                                    curShop.Money += curProduct.Value.Price;
                                }
                            }
                            else
                            {
                                throw new Exception("You can't buy all properties!");
                            }
                        }
                        else
                        {
                            throw new Exception("Not enough properties!");
                        }
                    }
                }
            }

        public void ChangeProductPrice(Shop shop, string name, int newPrice)
            {
                shop.ChangePrice(name, newPrice);
            }
    }
}