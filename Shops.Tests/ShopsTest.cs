using NUnit.Framework;
using Shops.Modules;
using Shops.Services;
using Shops.Tools;


namespace Shops.Tests
{
    public class Test
    {
        private ShopsManagment _shopsManagment;

        [SetUp]
        public void Setup()
        {
            _shopsManagment = new ShopsManagment();
        }

        [Test]
        public void AddShop_AddShopAndTransferProduct()
        {
            Shop testShop1 = _shopsManagment.AddShop("lenta","akademikov 8");
            Shop testShop2 = _shopsManagment.AddShop("lenta","akademikov 8");
            //добавить регист как в 1с yep
            //сначала добавляем туда продукты yep
            //а только потом в магазин в нужном количестве с нужной ценой 
            Product testProduct = _shopsManagment.AddProduct("Coke");
            _shopsManagment.ProductTransfer(testShop1, "Coke", 20, 100);

            Assert.Contains(testShop2.Id, new[] {2});
            Assert.Contains(testShop1.IsContais(testShop1, "Coke"), new[] {true});
        }

        [Test]
        public void ChangePrice_ChangePrice()
        {
            int newPrice = 40;
            Shop testShop1 = _shopsManagment.AddShop("lenta","akademikov 8");
            Product testProduct = _shopsManagment.AddProduct("Coke");
            _shopsManagment.ProductTransfer(testShop1, "Coke", 20, 10);
            testShop1.ChangePrice(testProduct.ProductName, newPrice);

            Assert.AreEqual(testShop1.GetPrice(testShop1, testProduct.ProductName), newPrice);
        }

        [Test]
        public void BuyProduct_CreateShopAndCreateProductAndBuy()
        {
            Shop testShop1 = _shopsManagment.AddShop("lenta","akademikov 8");
            Product testProduct1 = _shopsManagment.AddProduct( "Coke");
            _shopsManagment.ProductTransfer(testShop1, "Coke", 20, 10);
            testShop1.ChangePrice(testProduct1.ProductName, 40);

            Shop shopWithMinValue = _shopsManagment.FindShop(testProduct1.ProductName);


            Assert.Contains(shopWithMinValue.Name, new[] {"lenta"});
        }

        [Test]
        public void BuySomeProducts_CreateShopAndCreateProductsAndBuy()
        {
            Shop testShop1 = _shopsManagment.AddShop("lenta","akademikov 8");
            Product testProduct1 = _shopsManagment.AddProduct( "Coke");
            _shopsManagment.ProductTransfer(testShop1, "Coke", 10, 100);
            
            _shopsManagment.BuyProduct("Coke", 10);
        }
    }
}