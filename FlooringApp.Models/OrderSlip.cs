using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Models
{
    public class OrderSlip
    {
        public DateTime Date { get; set; }
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public State State { get; set; }
        public Product ProductType { get; set; }
        public decimal Area { get; set; }

        public OrderSlip()
        {
            State = new State();
            ProductType = new Product();
        }
    }
}
