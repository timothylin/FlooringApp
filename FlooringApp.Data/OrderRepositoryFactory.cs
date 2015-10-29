using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Data
{
    public static class OrderRepositoryFactory
    {
        public static IOrderRepository CreateOrderRepository()
        {
            var projectMode = ConfigurationManager.AppSettings["mode"];

            switch (projectMode)
            {
                case "prod":
                    return new OrderRepositoryProdMode();
                case "test":
                    return new OrderRepositoryTestMode();
                default:
                    throw new NotSupportedException(String.Format("{0} not supported!  You suck.", projectMode));
            }
        }
    }
}

