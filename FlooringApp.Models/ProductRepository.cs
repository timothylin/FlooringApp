using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Models
{
    public abstract class ProductRepository : IProductsRepository
    {
        protected List<Product> Products { get; set; } = new List<Product>();

        protected string FilePath = @"DataFiles\Products.txt";

        public virtual List<Product> GetAllProducts()
        {


            try
            {
                var reader = File.ReadAllLines(FilePath);

                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');

                    var product = new Product();

                    product.ProductType = columns[0];
                    product.CostPerSquareFoot = decimal.Parse(columns[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Product Repo - GetAllProducts");
            }

            return Products;
        }

        public virtual Product GetProduct(Product product)
        {
            var productToReturn = new Product();

            try
            {
                List<Product> products = GetAllProducts();
                productToReturn = products.FirstOrDefault(p => p.ProductType.ToUpper() == product.ProductType.ToUpper());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Product Repo - GetProduct");
            }

            return productToReturn;
        }

        public string ToCSVForTesting(Product product)
        {
            return String.Format("{0},{1},{2}", product.ProductType, product.CostPerSquareFoot, product.LaborCostPerSquareFoot);

        }

        public Product LoadFromCSVForTesting(string productCSV)
        {

            var columns = productCSV.Split(',');

            var product = new Product();

            product.ProductType = columns[0];
            product.CostPerSquareFoot = decimal.Parse(columns[1]);
            product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

            return product;
        }
    }
}
