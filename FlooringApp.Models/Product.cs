using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Models
{
    public class Product
    {
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }
        public string ProductType { get; set; }
    }
}
