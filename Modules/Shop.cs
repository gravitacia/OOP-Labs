using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Modules
{
    public class Shop
    {
        private Dictionary<int, ProductProperty> _productsList;
        public Shop(string shopName, int id, string addres, int money)
        {
            Name = shopName;
            Id = id;
            Addres = addres;
            Money = money;
            _productsList = new Dictionary<int, ProductProperty>();
        }

        public int Money { get; set; }

        public string Addres { get; set; }

        public string Name { get; set; }
        public int Id { get; set; }

        public void AddProductToShop(Product product, ProductProperty productProperty)
        {
            _productsList.Add(product.Id, productProperty);
        }

        public ProductProperty PriceAndCount(Product product, int price, int count)
        {
            var productProperty = new ProductProperty(product, price, count);
            return productProperty;
        }

        public Dictionary<int, ProductProperty> GetProductList()
        {
            return _productsList;
        }

        public void RemoveProductFromShop(int id)
        {
            _productsList.Remove(id);
        }

        public int ChangePrice(string name, int newPrice)
        {
            foreach (KeyValuePair<int, ProductProperty> curProduct in _productsList)
            {
                    if (curProduct.Value.RefProduct == name)
                    {
                        curProduct.Value.Price = newPrice;
                    }

                    return curProduct.Key;
            }

            throw new Exception("Warning");
        }

        public int GetPrice(Shop shop, string name)
        {
            foreach (var curProduct in shop._productsList.Where(curProduct => curProduct.Value.RefProduct == name))
            {
                return curProduct.Value.Price;
            }

            throw new Exception("Warning");
        }

        public bool IsContains(Shop shop, string name)
        {
            foreach (var curProduct in shop._productsList)
            {
                if (curProduct.Value.RefProduct == name)
                {
                    return true;
                }
            }

            throw new Exception("Warning!");
        }
    }
}