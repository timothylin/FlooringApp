using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Models;

namespace FlooringApp.UI
{
    public static class DisplayOrderInformation
    {
        public static void DisplayRepoInfo(OrderSlip orderQuery)
        {
            Logger.Info("Display Order Repo Info", "DisplayOrderInformation.DisplayRepoInfo");

            var ops = new OrderOperations();
            var response = ops.GetAllOrders(orderQuery.Date);

            Console.Clear();
            if (response.Success)
            {
                foreach (var order in response.OrderList)
                {
                    Console.WriteLine("Order Number: {0}\n Customer Name: {1}\n State: {2}\n Tax Rate: {3:p}\n Product Type: {4}\n Area: {5}\n Cost per Square Foot: {6:c}\n Labor Cost Per Square Foot: {7:c}\n Material Cost: {8:c}\n Labor Cost: {9:c}\n Total Tax: {10:c}\n Total Cost: {11:c}",
                        order.OrderNumber, order.CustomerName, order.State.StateAbbreviation, order.TaxRate / 100,
                        order.ProductType.ProductType, order.Area, order.CostPerSquareFoot, order.LaborCostPerSquareFoot,
                        order.MaterialCost, order.LaborCost, order.Tax, order.Total);
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine(response.Message);
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to return to Main menu...");
            Console.ReadKey();
        }

        public static void DisplayNewOrderInfo(Order order)
        {
            Logger.Info("Display New Order Information", "DisplayOrderInformation.DisplayNewOrderInfo");

            Console.Clear();
            Console.WriteLine("Customer Name: {0}\n State: {1}\n Tax Rate: {2:p}\n Product Type: {3}\n Area: {4}\n Cost per Square Foot: {5:c}\n Labor Cost per Square Foot: {6:c}\n Material Cost: {7:c}\n Labor Cost: {8:c}\n Total Taxes: {9:c}\n Total Cost: {10:c}",
                order.CustomerName, order.State.StateAbbreviation, order.TaxRate / 100, order.ProductType.ProductType, order.Area,
                order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
            Console.WriteLine();
        }

        public static void DisplayNewOrderConfirmation(Order order)
        {
            Logger.Info("Display New Order Information for Confirmation", "DisplayOrderInformation.DisplayNewOrderConfirmation");

            Console.Clear();
            Console.WriteLine("Order Number: {0}\n Customer Name: {1}\n State: {2}\n Tax Rate: {3:p}\n Product Type: {4}\n Area: {5}\n Cost per Square Foot: {6:c}\n Labor Cost Per Square Foot: {7:c}\n Material Cost: {8:c}\n Labor Cost: {9:c}\n Total Tax: {10:c}\n Total Cost: {11:c}",
                order.OrderNumber, order.CustomerName, order.State.StateAbbreviation, order.TaxRate / 100, order.ProductType.ProductType, order.Area,
                order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
            Console.WriteLine();

            Console.WriteLine("Order saved. Press any key to return to Main menu...");
            Console.ReadKey();
        }

        public static void DisplayEditOrderInfo(Order order)
        {
            Logger.Info("Display Edit Order Information for Confirmation", "DisplayOrderInformation.DisplayEditOrderInfo");

            Console.Clear();
            Console.WriteLine("Order Number: {0}\n Customer Name: {1}\n State: {2}\n Tax Rate: {3:p}\n Product Type: {4}\n Area: {5}\n Cost per Square Foot: {6:c}\n Labor Cost Per Square Foot: {7:c}\n Material Cost: {8:c}\n Labor Cost: {9:c}\n Total Tax: {10:c}\n Total Cost: {11:c}",
                order.OrderNumber, order.CustomerName, order.State.StateAbbreviation, order.TaxRate / 100, order.ProductType.ProductType, order.Area,
                order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
            Console.WriteLine();

            Console.WriteLine("Press any key to return to Main menu...");
            Console.ReadKey();
        }

        public static void DisplayRemoveOrderInfo(Order order)
        {
            Logger.Info("Display Removal Order Information for Confirmation", "DisplayOrderInformation.DisplayRemoveOrderConfirmation");
            Console.Clear();
            Console.WriteLine("Order Number: {0}\n Customer Name: {1}\n State: {2}\n Tax Rate: {3:p}\n Product Type: {4}\n Area: {5}\n Cost per Square Foot: {6:c}\n Labor Cost Per Square Foot: {7:c}\n Material Cost: {8:c}\n Labor Cost: {9:c}\n Total Tax: {10:c}\n Total Cost: {11:c}",
                order.OrderNumber, order.CustomerName, order.State.StateAbbreviation, order.TaxRate / 100, order.ProductType.ProductType, order.Area,
                order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
            Console.WriteLine();
        }
    }
}
