using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Modules
{
    public class Shop
    {
        private Dictionary<Product, ProductProperty> _productsList;
        public Shop(string shopName, int id, string addres, int money)
        {
            Name = shopName;
            Id = id;
            Addres = addres;
            Money = money;
            _productsList = new Dictionary<Product, ProductProperty>();
        }

        public int Money { get; set; }

        public string Addres { get; set; }

        public string Name { get; set; }
        public int Id { get; set; }

        public void AddProductToShop(Product product, ProductProperty productProperty)
        {
            _productsList.Add(product, productProperty);
        }

        public ProductProperty PriceAndCount(int price, int count)
        {
            var productProperty = new ProductProperty(price, count);
            return productProperty;
        }

        public Dictionary<Product, ProductProperty> GetProductList()
        {
            return _productsList;
        }

        public void RemoveProductFromShop(Product product)
        {
            _productsList.Remove(product);
        }

        public Product ChangePrice(string name, int newPrice)
        {
            foreach (KeyValuePair<Product, ProductProperty> curProduct in _productsList)
            {
                    if (curProduct.Key.ProductName == name)
                    {
                        curProduct.Value.Price = newPrice;
                    }

                    return curProduct.Key;
            }

            throw new Exception("Warning");
        }

        public int GetPrice(Shop shop, string name)
        {
            foreach (var curProduct in shop._productsList.Where(curProduct => curProduct.Key.ProductName == name))
            {
                return curProduct.Value.Price;
            }

            throw new Exception("Warning");
        }

        public bool IsContais(Shop shop, string name)
        {
            foreach (var curProduct in shop._productsList)
            {
                if (curProduct.Key.ProductName == name)
                {
                    return true;
                }
            }

            throw new Exception("Warning!");
        }
    }
}