using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public State State { get; set; }
        public decimal TaxRate { get; set; }
        public Product ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public Order()
        {
            State = new State();
            ProductType = new Product();
        }
    }
}
