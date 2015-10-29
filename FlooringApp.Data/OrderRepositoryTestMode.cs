using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Data
{
    public class OrderRepositoryTestMode : OrderRepository
    {
        public OrderRepositoryTestMode()
        {
            Orders.AddRange(new List<Order>()
            {
                new Order()
                {
                    OrderNumber = 1,
                    CustomerName = "Wise",
                    State = new State()
                    {
                        StateAbbreviation = "OH",
                        StateName = "Ohio",
                        TaxRate = 6.25M
                    },
                    TaxRate = 6.25M,
                    ProductType = new Product()
                    {
                        CostPerSquareFoot = 5.15M,
                        LaborCostPerSquareFoot = 4.75M,
                        ProductType = "WOOD"
                    },
                    Area = 100.00M,
                    CostPerSquareFoot = 5.15M,
                    LaborCostPerSquareFoot = 4.75M,
                    MaterialCost = 515.00M,
                    LaborCost = 475.00M,
                    Tax = 61.88M,
                    Total = 1051.88M
                },

                new Order()
                {
                    OrderNumber = 2,
                    CustomerName = "Ward",
                    State = new State()
                    {
                        StateAbbreviation = "MI",
                        StateName = "Michigan",
                        TaxRate = 5.75M
                    },
                    TaxRate = 5.75M,
                    ProductType = new Product()
                    {
                        CostPerSquareFoot = 2.25M,
                        LaborCostPerSquareFoot = 2.10M,
                        ProductType = "CARPET"
                    },
                    Area = 250.24M,
                    CostPerSquareFoot = 2.25M,
                    LaborCostPerSquareFoot = 2.10M,
                    MaterialCost = 563.0400M,
                    LaborCost = 525.5040M,
                    Tax = 62.59128000M,
                    Total = 1151.13528000M
                },

                new Order()
                {
                    OrderNumber = 3,
                    CustomerName = "Wise, Eric",
                    State = new State()
                    {
                        StateAbbreviation = "PA",
                        StateName = "Pennsylvania",
                        TaxRate = 6.75M
                    },
                    TaxRate = 6.75M,
                    ProductType = new Product()
                    {
                        CostPerSquareFoot = 1.75M,
                        LaborCostPerSquareFoot = 2.10M,
                        ProductType = "LAMINATE"
                    },
                    Area = 0.75M,
                    CostPerSquareFoot = 1.75M,
                    LaborCostPerSquareFoot = 2.10M,
                    MaterialCost = 1.3125M,
                    LaborCost = 1.5750M,
                    Tax = 0.19490625M,
                    Total = 3.08240625M
                },

                new Order()
                {
                    OrderNumber = 4,
                    CustomerName = "Ward, Eric",
                    State = new State()
                    {
                        StateAbbreviation = "IN",
                        StateName = "Indiana",
                        TaxRate = 6.00M
                    },
                    TaxRate = 6.00M,
                    ProductType = new Product()
                    {
                        CostPerSquareFoot = 3.50M,
                        LaborCostPerSquareFoot = 4.15M,
                        ProductType = "TILE"
                    },
                    Area = 345.12M,
                    CostPerSquareFoot = 3.50M,
                    LaborCostPerSquareFoot = 4.15M,
                    MaterialCost = 1207.9200M,
                    LaborCost = 1432.2480M,
                    Tax = 158.410080M,
                    Total = 2798.578080M
                },
            });
        }

        public override List<Order> GetAllOrders(DateTime Date)
        {
            return Orders;
        }

        public override void UpdateOrder(Order OrderToUpdate, DateTime Date)
        {
            var orders = GetAllOrders(Date);
            var order = orders.First(o => o.OrderNumber == OrderToUpdate.OrderNumber);

            order.CustomerName = OrderToUpdate.CustomerName;
            order.State = OrderToUpdate.State;
            order.TaxRate = OrderToUpdate.TaxRate;
            order.ProductType = OrderToUpdate.ProductType;
            order.Area = OrderToUpdate.Area;
            order.CostPerSquareFoot = OrderToUpdate.CostPerSquareFoot;
            order.LaborCostPerSquareFoot = OrderToUpdate.LaborCostPerSquareFoot;
            order.MaterialCost = OrderToUpdate.MaterialCost;
            order.LaborCost = OrderToUpdate.LaborCost;
            order.Tax = OrderToUpdate.Tax;
            order.Total = OrderToUpdate.Total;

            OverwriteFile(Orders, Date);
        }

        public override void OverwriteFile(List<Order> orders, DateTime Date)
        {
            Orders = orders;
        }

        public override void WriteNewLine(Order order, DateTime Date)
        {
            Orders.Add(order);
        }

        public override bool CheckIfRepositoryExists(DateTime date)
        {
            return true;
        }
    }
}
