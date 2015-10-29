using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Data;
using FlooringApp.Data.States;
using FlooringApp.Data.Products;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.BLL
{
    public class OrderOperations
    {

        private IOrderRepository _orderRepository;
        private IProductsRepository _productsRepository;
        private IStateRepository _statesRepository;
        private Response _response;

        public OrderOperations()
        {
            _orderRepository = OrderRepositoryFactory.CreateOrderRepository();
            _productsRepository = ProductsRepositoryFactory.CreateProductsRepository();
            _statesRepository = StatesRepositoryFactory.CreateStatesRepository();
            _response = new Response();
        }

        public Response GetAllOrders(DateTime Date)
        {

            Logger.Info("Get all order called", "OrderOps - GetAllOrders");

            if (!_orderRepository.CheckIfRepositoryExists(Date))
            {
                _response.Success = false;
                _response.Message = "There are no orders for this date!";
                Logger.Warning("User entered a valid date that has no corresponding repo", "OrderOps - GetAllOrders");
            }
            else
            {
                var ordersList = _orderRepository.GetAllOrders(Date);
                _response.Success = true;
                _response.OrderList = ordersList;
            }

            return _response;
        }

        public Response GetOrder(int OrderNumber, DateTime Date)
        {
            Logger.Info("Get order called", "OrderOps - GetOrders");

            if (!_orderRepository.CheckIfRepositoryExists(Date))
            {
                _response.Success = false;
                _response.Message = "No repository exists for this date.";
                Logger.Warning("User entered a valid date and order number but no repo exists for that date", "OrderOps - GetOrder");
            }
            else
            {
                var order = _orderRepository.GetOrder(OrderNumber, Date);

                if (order == null)
                {
                    _response.Success = false;
                    _response.Message = "Order not found.";
                    Logger.Warning("User entered a valid date and order number but no order exists for that date", "OrderOps - GetOrder");
                }
                else
                {
                    _response.Success = true;
                    _response.Order = order;
                }
            }

            return _response;
        }

        public Response CreateOrder(OrderSlip slip)
        {
            Logger.Info("Create order called", "OrderOps - CreateOrder");

            var orderToCreate = new Order();

            orderToCreate.CustomerName = slip.CustomerName;
            orderToCreate.State = slip.State;
            orderToCreate.ProductType = slip.ProductType;
            orderToCreate.Area = slip.Area;

            orderToCreate = CalculateOrderTotals(orderToCreate, orderToCreate.State, orderToCreate.ProductType);

            _response.Order = orderToCreate;
            _response.Success = true;

            return _response;
        }

        public void EditOrder(Order order, DateTime date)
        {
            Logger.Info("Edit order called", "OrderOps - EditOrder");

            order = CalculateOrderTotals(order, order.State, order.ProductType);
            _orderRepository.UpdateOrder(order, date);
        }

        public void RemoveOrder(int OrderNumber, DateTime Date)
        {
            Logger.Info("Remove order called", "OrderOps - RemoveOrder");

            var orders = GetAllOrders(Date);
            orders.OrderList.RemoveAll(o => o.OrderNumber == OrderNumber);
            _orderRepository.OverwriteFile(orders.OrderList, Date);
        }

        public void WriteNewOrderToRepository(Response response)
        {
            Logger.Info("Writing new order to repo", "OrderOps - WriteNewOrderToRepo");

            List<Order> orders = new List<Order>();

            if (!_orderRepository.CheckIfRepositoryExists(response.Slip.Date))
            {
                Logger.Info("Repo for this date does not exist, new repo created", "OrderOps - WriteNewOrderToRepo");

                response.Order.OrderNumber = 1;
                orders.Add(response.Order);
                _orderRepository.OverwriteFile(orders, response.Slip.Date);
            }
            else
            {
                Logger.Info("Repo for this date exists, add new line to file", "OrderOps - WriteNewOrderToRepo");

                response.Order.OrderNumber = NewOrderNumber(response.Slip.Date);
                _orderRepository.WriteNewLine(response.Order, response.Slip.Date);
            }
        }

        public Order CalculateOrderTotals(Order Order, State state, Product product)
        {
            Logger.Info("Calculate order Totals called", "OrderOps - CalculateOrderTotals");

            Order.CostPerSquareFoot = _productsRepository.GetProduct(product).CostPerSquareFoot;
            Order.LaborCostPerSquareFoot = _productsRepository.GetProduct(product).LaborCostPerSquareFoot;
            Order.TaxRate = _statesRepository.GetState(state).TaxRate;
            Order.LaborCost = Order.LaborCostPerSquareFoot * Order.Area;
            Order.MaterialCost = Order.CostPerSquareFoot * Order.Area;
            Order.Tax = (Order.MaterialCost + Order.LaborCost) * (Order.TaxRate / 100);
            Order.Total = Order.LaborCost + Order.MaterialCost + Order.Tax;

            return Order;
        }

        public int NewOrderNumber(DateTime date)
        {
            Logger.Info("New order number called", "OrderOps - NewOrderNumber");

            var allOrders = _orderRepository.GetAllOrders(date);

            if (allOrders.Count == 0)
            {
                return 1;
            }
            else
            {
                return allOrders.OrderBy(o => o.OrderNumber).Max(x => x.OrderNumber) + 1;
            }
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
