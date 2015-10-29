using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public bool Updated { get; set; }
        public bool UserResponse { get; set; }
        public string Message { get; set; }
        public Order Order { get; set; }
        public OrderSlip Slip { get; set; }
        public List<Order> OrderList { get; set; } 
        public State State { get; set; }
        public Product Product { get; set; }

        public Response()
        {
            Message = "";
            Order = new Order();
            Slip = new OrderSlip();
            OrderList = new List<Order>();
            State = new State();
            Product = new Product();
        }
    }
}
