using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;
using System.IO;

namespace FlooringApp.Models
{

    public abstract class OrderRepository : IOrderRepository
    {

        protected List<Order> Orders { get; set; } = new List<Order>();
        protected string FilePath = @"DataFiles\Orders_";

        public virtual List<Order> GetAllOrders(DateTime Date)
        {

            try
            {
                Orders = new List<Order>();

                var reader = File.ReadAllLines(FilePath + Date.ToString("MMddyyy") + ".txt");

                for (int i = 1; i < reader.Length; i++)
                {
                    var firstIndexOfName = reader[i].IndexOf("\"");
                    var lastIndexOfName = reader[i].LastIndexOf("\"");

                    var order = new Order();

                    order.OrderNumber = int.Parse(reader[i].Substring(0, firstIndexOfName - 1));
                    order.CustomerName = reader[i].Substring(firstIndexOfName + 1, lastIndexOfName - firstIndexOfName - 1);

                    var columnsToParse = reader[i].Substring(lastIndexOfName + 2);
                    var columns = columnsToParse.Split(',');

                    order.State.StateAbbreviation = columns[0];
                    order.TaxRate = decimal.Parse(columns[1]);
                    order.ProductType.ProductType = columns[2];
                    order.Area = decimal.Parse(columns[3]);
                    order.CostPerSquareFoot = decimal.Parse(columns[4]);
                    order.LaborCostPerSquareFoot = decimal.Parse(columns[5]);
                    order.MaterialCost = decimal.Parse(columns[6]);
                    order.LaborCost = decimal.Parse(columns[7]);
                    order.Tax = decimal.Parse(columns[8]);
                    order.Total = decimal.Parse(columns[9]);

                    Orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Order Repo - GetAllOrders");
            }
            return Orders;
        }

        public virtual Order GetOrder(int OrderNumber, DateTime Date)
        {
            var order = new Order();

            try
            {
                List<Order> orders = GetAllOrders(Date);
                order = orders.FirstOrDefault(o => o.OrderNumber == OrderNumber);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Order Repo - GetOrder");
            }

            return order;

        }

        public virtual void UpdateOrder(Order OrderToUpdate, DateTime Date)
        {
            try
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

                OverwriteFile(orders, Date);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Order Repo - UpdateOrder");
            }

        }

        public virtual void OverwriteFile(List<Order> orders, DateTime Date)
        {
            try
            {
                File.Delete(FilePath);

                using (var writer = File.CreateText(FilePath + Date.ToString("MMddyyy") + ".txt"))
                {
                    writer.WriteLine
                        ("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot," +
                         "LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                    foreach (var order in orders)
                    {
                        writer.WriteLine("{0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber,
                            order.CustomerName,
                            order.State.StateAbbreviation, order.TaxRate, order.ProductType.ProductType, order.Area,
                            order.CostPerSquareFoot,
                            order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
                    }

                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex, "Order Repo - OverwriteFile");
            }
        }

        public virtual void WriteNewLine(Order order, DateTime Date)
        {
            try
            {
                using (var writer = File.AppendText(FilePath + Date.ToString("MMddyyy") + ".txt"))
                {
                    writer.WriteLine("{0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber,
                        order.CustomerName, order.State.StateAbbreviation, order.TaxRate, order.ProductType.ProductType,
                        order.Area, order.CostPerSquareFoot,
                        order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Order Repo - WriteNewLine");
            }

        }


        public virtual bool CheckIfRepositoryExists(DateTime date)
        {
            bool fileExists = false;

            try
            {
                fileExists = File.Exists(FilePath + date.ToString("MMddyyyy") + ".txt");

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Order Repo - CheckIfRepoExists");

            }

            return fileExists;
        }

        public string ToCSVForTesting(Order order)
        {
            return String.Format("{0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber,
                        order.CustomerName, order.State.StateAbbreviation, order.TaxRate, order.ProductType.ProductType,
                        order.Area, order.CostPerSquareFoot,
                        order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
        }

        public Order LoadFromCSVForTesting(string orderCSV)
        {
            var order = new Order();

            var firstIndexOfName = orderCSV.IndexOf("\"");
            var lastIndexOfName = orderCSV.LastIndexOf("\"");

            order.OrderNumber = int.Parse(orderCSV.Substring(0, firstIndexOfName - 1));
            order.CustomerName = orderCSV.Substring(firstIndexOfName + 1, lastIndexOfName - firstIndexOfName - 1);

            var columnsToParse = orderCSV.Substring(lastIndexOfName + 2);
            var columns = columnsToParse.Split(',');

            order.State.StateAbbreviation = columns[0];
            order.TaxRate = decimal.Parse(columns[1]);
            order.ProductType.ProductType = columns[2];
            order.Area = decimal.Parse(columns[3]);
            order.CostPerSquareFoot = decimal.Parse(columns[4]);
            order.LaborCostPerSquareFoot = decimal.Parse(columns[5]);
            order.MaterialCost = decimal.Parse(columns[6]);
            order.LaborCost = decimal.Parse(columns[7]);
            order.Tax = decimal.Parse(columns[8]);
            order.Total = decimal.Parse(columns[9]);

            return order;
        }
    }
}
