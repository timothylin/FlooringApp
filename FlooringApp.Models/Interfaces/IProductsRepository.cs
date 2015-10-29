using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringApp.Models.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAllProducts();

        Product GetProduct(Product product);

        string ToCSVForTesting(Product product);

        Product LoadFromCSVForTesting(string productCSV);
    }
}
