using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Data.Products;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;
using NUnit.Framework;

namespace FlooringApp.Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private IProductsRepository _repo;
        private string[] _products;

        [SetUp]
        public void SetUp()
        {
            _repo = ProductsRepositoryFactory.CreateProductsRepository();
            _products = new[]
            {
                "Carpet,2.25,2.10",
                "Laminate,1.75,2.10",
                "Tile,3.50,4.15",
                "Wood,5.15,4.75"
            };
        }

        [Test]
        public void GetAllProductsTest()
        {
            var result = _repo.GetAllProducts();
            var actual = new string[4];

            for (int i = 0; i < result.Count; i++)
            {
                actual[i] = _repo.ToCSVForTesting(result[i]);
            }
            Assert.AreEqual(_products, actual);
        }

        [TestCase("CARPET", 0)]
        [TestCase("LAMINATE", 1)]
        [TestCase("TILE", 2)]
        [TestCase("WOOD", 3)]
        public void GetProductTest(string productType, int indexOfProduct)
        {
            // arrange
            Product product = new Product();
            product.ProductType = productType;

            // act
            var result = _repo.GetProduct(product);
            var actual = _repo.ToCSVForTesting(result);

            // assert
            Assert.AreEqual(_products[indexOfProduct], actual);

        }

    }
}
