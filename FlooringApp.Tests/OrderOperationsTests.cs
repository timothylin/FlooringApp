using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Data;
using FlooringApp.Models;
using NUnit.Framework;

namespace FlooringApp.Tests
{
    [TestFixture]
    public class OrderOperationsTests
    {

        OrderOperations ops;
        string[] orders;
        string[] editedOrders;
        string[] createdOrders;
        string[] allOrders;
        string projectMode;

        [SetUp]
        public void Setup()
        {
            ops = new OrderOperations();

            projectMode = ConfigurationManager.AppSettings["mode"];

            orders = new string[]
            {
             "1,\"Wise\",OH,6.25,WOOD,100.00,5.15,4.75,515.00,475.00,61.88,1051.88",
            "2,\"Ward\",MI,5.75,CARPET,250.24,2.25,2.10,563.0400,525.5040,62.59128000,1151.13528000",
            "3,\"Wise, Eric\",PA,6.75,LAMINATE,0.75,1.75,2.10,1.3125,1.5750,0.19490625,3.08240625",
            "4,\"Ward, Eric\",IN,6.00,TILE,345.12,3.50,4.15,1207.9200,1432.2480,158.410080,2798.578080",
            "5,\"James, LeBron\",OH,6.25,TILE,150.54,3.50,4.15,526.8900,624.7410,71.97693750,1223.60793750"
            };

            createdOrders = new string[1]
            {
                "5,\"James, LeBron\",OH,6.25,TILE,150.54,3.50,4.15,526.8900,624.7410,71.97693750,1223.60793750"
            };

            editedOrders = new string[5]
            {
                "1,\"Wise\",OH,6.25,WOOD,100.00,5.15,4.75,515.00,475.00,61.88,1051.88",
                "2,\"Ward\",MI,5.75,CARPET,250.24,2.25,2.10,563.0400,525.5040,62.59128000,1151.13528000",
                "3,\"Wise, Eric\",PA,6.75,LAMINATE,0.75,1.75,2.10,1.3125,1.5750,0.19490625,3.08240625",
                "4,\"Jones, Bob\",MI,5.75,LAMINATE,500.56,1.75,2.10,875.9800,1051.1760,110.81147000,2037.96747000",
                "5,\"James, LeBron\",OH,6.25,TILE,150.54,3.50,4.15,526.8900,624.7410,71.97693750,1223.60793750"
            };

            allOrders = new string[4]
            {
                "1,\"Wise\",OH,6.25,WOOD,100.00,5.15,4.75,515.00,475.00,61.88,1051.88",
                "2,\"Ward\",MI,5.75,CARPET,250.24,2.25,2.10,563.0400,525.5040,62.59128000,1151.13528000",
                "3,\"Wise, Eric\",PA,6.75,LAMINATE,0.75,1.75,2.10,1.3125,1.5750,0.19490625,3.08240625",
                "4,\"Ward, Eric\",IN,6.00,TILE,345.12,3.50,4.15,1207.9200,1432.2480,158.410080,2798.578080",
            };

        }

        [Test]
        public void NextOrderNumberTest() // Testing for the maximum lines.
        {

            var number = ops.NewOrderNumber(new DateTime(2013, 6, 1));

            if (projectMode == "prod")
            {
                Assert.AreEqual(6, number);
            }
            else if (projectMode == "test")
            {
                Assert.AreEqual(5, number);
            }
        }

        [TestCase("6/1/2013", "James, LeBron", "OH", "TILE", 150.54, 0)]
        public void CreateOrderTest(DateTime date, string CustomerName, string State, string ProductType, decimal Area, int orderIndex)
        {
            // arrange
            var slip = new OrderSlip();
            Response response = new Response();

            slip.CustomerName = CustomerName;
            slip.State.StateAbbreviation = State;
            slip.ProductType.ProductType = ProductType;
            slip.Area = Area;

            // act

            response = ops.CreateOrder(slip);
            response.Slip.Date = date;
            ops.WriteNewOrderToRepository(response);

            string order = ops.ToCSVForTesting(response.Order);

            // assert

            Assert.AreEqual(order, createdOrders[orderIndex]);
        }

        //[TestCase("06/01/2013", 2, true)]
        //private void RemoveOrderTest(DateTime CusDate, int CusOrderNumber)
        //{
        //    var ops = new OrderOperations();
        //    var order = new Order();

        //    Response orderExists = ops.GetOrder(CusOrderNumber, CusDate);

        //    //Initial Results
        //    Console.WriteLine(orderExists);

        [TestCase(1, "2013/6/1", 0)]
        [TestCase(2, "2013/6/1", 1)]
        [TestCase(3, "2013/6/1", 2)]
        public void GetOrderValidInputsTest(int orderNumber, DateTime date, int indexOfOrdersArray)
        {
            // arrange
            Response response = new Response();

            // act
            response = ops.GetOrder(orderNumber, date);
            string orderCSV = ops.ToCSVForTesting(response.Order);

            // assert
            Assert.AreEqual(orders[indexOfOrdersArray], orderCSV);
        }

        [TestCase(10, "2013/6/1", false)]
        [TestCase(1999, "2016/3/25", false)]
        public void GetOrderInvalidInputsTest(int orderNumber, DateTime date, bool expected)
        {
            // arrange
            Response response = new Response();

            // act
            response = ops.GetOrder(orderNumber, date);

            // assert
            Assert.AreEqual(expected, response.Success);
        }

        [TestCase("06/01/2013", 4, "Jones, Bob", "MI", "LAMINATE", 500.56, 3, 3, true)]
        public void EditOrderTest(DateTime Date, int OrderNumber, string Name, string State, string ProductType, decimal Area, int IndexOfOrder, int IndexOfEditedOrder, bool expected)
        {
            // arrange
            var response = new Response();
            var order = ops.LoadFromCSVForTesting(string.Format("{0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", OrderNumber, Name, State, 0, ProductType, Area, 0, 0, 0, 0, 0, 0));

            // act
            ops.EditOrder(order, Date);
            response = ops.GetOrder(OrderNumber, Date);
            string orderOutput = ops.ToCSVForTesting(response.Order);

            // assert
            Assert.AreEqual(expected, response.Success);
            Assert.AreEqual(editedOrders[IndexOfEditedOrder], orderOutput);
        }

        [TestCase("06/01/2013", 2, true, false)]
        public void RemoveOrderTest(DateTime Date, int OrderNumber, bool OrderExists, bool IsOrderStillThere)
        {
            var response = new Response();
            //var order = ops.LoadFromCSVForTesting(string.Format("{0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", OrderNumber, Name, State, 0, ProductType, Area, 0, 0, 0, 0, 0, 0));

            //Check to make sure it exists.
            response = ops.GetOrder(OrderNumber, Date);
            Assert.AreEqual(response.Success, OrderExists);

            //act
            ops.RemoveOrder(OrderNumber, Date);

            //Make sure order is no longer there. Return Message should be false.
            response = ops.GetOrder(OrderNumber, Date);
            Assert.AreEqual(response.Success, IsOrderStillThere);
        }

        [Test]
        public void GetAllOrdersTest()
        {
            // arrange
            Response response = new Response();
            string[] orderCSV;

            // act
            response = ops.GetAllOrders(new DateTime(2013, 6, 1));



            if (projectMode == "prod")
            {
                orderCSV = new string[5];

                for (int i = 0; i < response.OrderList.Count; i++)
                {
                    orderCSV[i] = ops.ToCSVForTesting(response.OrderList[i]);
                }

                // assert
                Assert.AreEqual(editedOrders, orderCSV);
            }
            else if (projectMode == "test")
            {
                orderCSV = new string[4];

                for (int i = 0; i < response.OrderList.Count; i++)
                {
                    orderCSV[i] = ops.ToCSVForTesting(response.OrderList[i]);
                }

                // assert
                Assert.AreEqual(allOrders, orderCSV);
            }
        }

        [TestCase(1, false)]
        [TestCase(10, false)]
        public void RemoveOrderTest(int orderNumber, bool expected)
        {
            // arrange
            Response response = new Response();

            // act
            ops.RemoveOrder(orderNumber, new DateTime(2013, 6, 1));
            response = ops.GetOrder(orderNumber, new DateTime(2013, 6, 1));

            // assert
            Assert.AreEqual(expected, response.Success);
        }
    }
}
