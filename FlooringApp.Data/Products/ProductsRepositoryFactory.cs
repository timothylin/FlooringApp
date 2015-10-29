using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Data.Products
{
    public class ProductsRepositoryFactory
    {
        public static IProductsRepository CreateProductsRepository()
        {
            var projectMode = ConfigurationManager.AppSettings["mode"];

            switch (projectMode)
            {
                case "prod":
                    return new ProductsRepositoryProdMode();
                case "test":
                    return new ProductsRepositoryTestMode();
                default:
                    throw new NotSupportedException(String.Format("{0} not supported!  You suck.", projectMode));
            }
        }
    }
}
