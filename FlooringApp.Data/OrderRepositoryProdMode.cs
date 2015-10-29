using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Data
{
    public class OrderRepositoryProdMode : OrderRepository
    {
        public override List<Order> GetAllOrders(DateTime Date)
        {
            return base.GetAllOrders(Date);
        }
    }
}
