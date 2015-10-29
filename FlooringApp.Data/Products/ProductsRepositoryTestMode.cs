using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models;

namespace FlooringApp.Data.Products
{
    public class ProductsRepositoryTestMode : ProductRepository
    {
        public ProductsRepositoryTestMode() : base()
        {
            Products.AddRange(new List<Product>()
            {
                new Product()
                {
                    ProductType = "Carpet",
                    CostPerSquareFoot = 2.25M,
                    LaborCostPerSquareFoot = 2.10M
                },
                new Product()
                {
                    ProductType = "Laminate",
                    CostPerSquareFoot = 1.75M,
                    LaborCostPerSquareFoot = 2.10M
                },
                new Product()
                {
                    ProductType = "Tile",
                    CostPerSquareFoot = 3.50M,
                    LaborCostPerSquareFoot = 4.15M
                },
                new Product()
                {
                    ProductType = "Wood",
                    CostPerSquareFoot = 5.15M,
                    LaborCostPerSquareFoot = 4.75M
                }
            });
        }

        public override List<Product> GetAllProducts()
        {
            return Products;
        }

        public override Product GetProduct(Product product)
        {
            List<Product> products = GetAllProducts();
            return products.FirstOrDefault(p => p.ProductType.ToUpper() == product.ProductType.ToUpper());
        }
    }
}
