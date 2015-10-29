using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Data;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;
using NUnit.Framework;

namespace FlooringApp.Tests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private IOrderRepository _repo;
        private string[] _orders;
        private string[] _allOrders;
        private string[] _ordersWithNewLineTest;
        private string[] _ordersWithNewLineProd;
        private string _orderToUpdate;
        private string _orderToAdd;
        string projectMode;

        [SetUp]
        public void SetUp()
        {
            _repo = OrderRepositoryFactory.CreateOrderRepository();
            projectMode = ConfigurationManager.AppSettings["mode"];

            _orders = new[]
            {
                "1,\"Wise\",OH,6.25,WOOD,100.00,5.15,4.75,515.00,475.00,61.88,1051.88",
                "2,\"Ward\",MI,5.75,CARPET,250.24,2.25,2.10,563.0400,525.5040,62.59128000,1151.13528000",
                "3,\"Wise, Eric\",PA,6.75,LAMINATE,0.75,1.75,2.10,1.3125,1.5750,0.19490625,3.08240625",
                "4,\"Ward, Eric\",IN,6.00,TILE,345.12,3.50,4.15,1207.9200,1432.2480,158.410080,2798.578080"
             };

            _orderToUpdate =
                "4,\"James, LeBron\",OH,6.25,TILE,150.54,3.50,4.15,526.8900,624.7410,71.97693750,1223.60793750";

            _orderToAdd =
                "5,\"James, LeBron\",OH,6.25,TILE,150.54,3.50,4.15,526.8900,624.7410,71.97693750,1223.60793750";

            _allOrders = new[]
{
                "1,\"Wise\",OH,6.25,WOOD,100.00,5.15,4.75,515.00,475.00,61.88,1051.88",
                "2,\"Ward\",MI,5.75,CARPET,250.24,2.25,2.10,563.0400,525.5040,62.59128000,1151.13528000",
                "4,\"Ward, Eric\",IN,6.00,TILE,345.12,3.50,4.15,1207.9200,1432.2480,158.410080,2798.578080"
             };

            _ordersWithNewLineTest = new[]
{
                "1,\"Wise\",OH,6.25,WOOD,100.00,5.15,4.75,515.00,475.00,61.88,1051.88",
                "2,\"Ward\",MI,5.75,CARPET,250.24,2.25,2.10,563.0400,525.5040,62.59128000,1151.13528000",
                  "3,\"Wise, Eric\",PA,6.75,LAMINATE,0.75,1.75,2.10,1.3125,1.5750,0.19490625,3.08240625",
                "4,\"Ward, Eric\",IN,6.00,TILE,345.12,3.50,4.15,1207.9200,1432.2480,158.410080,2798.578080",
                "5,\"James, LeBron\",OH,6.25,TILE,150.54,3.50,4.15,526.8900,624.7410,71.97693750,1223.60793750"
        };

            _ordersWithNewLineProd = new[]
{
                "1,\"Wise\",OH,6.25,WOOD,100.00,5.15,4.75,515.00,475.00,61.88,1051.88",
                "2,\"Ward\",MI,5.75,CARPET,250.24,2.25,2.10,563.0400,525.5040,62.59128000,1151.13528000",
                "4,\"James, LeBron\",OH,6.25,TILE,150.54,3.50,4.15,526.8900,624.7410,71.97693750,1223.60793750",
                "5,\"James, LeBron\",OH,6.25,TILE,150.54,3.50,4.15,526.8900,624.7410,71.97693750,1223.60793750"
        };

        }

        [Test]
        public void CanGetAllOrders()
        {
            var orders = _repo.GetAllOrders(new DateTime(2013, 06, 01));

            //Check for number of orders in Test file.
            Assert.AreEqual(orders.Count, 4);
        }

        [Test]
        public void CanGetOrders()
        {
            var order = _repo.GetOrder(1, new DateTime(2013, 06, 01));

            //Check if Orders is returning proper values.
            Assert.AreEqual(1, order.OrderNumber);
            Assert.AreEqual("Wise", order.CustomerName);
            Assert.AreEqual("OH", order.State.StateAbbreviation);
            Assert.AreEqual("WOOD", order.ProductType.ProductType);
        }

        [Test]
        public void GetAllOrdersTest()
        {
            var result = _repo.GetAllOrders(new DateTime(2013, 6, 1));
            var actual = new string[4];

            for (int i = 0; i < result.Count; i++)
            {
                actual[i] = _repo.ToCSVForTesting(result[i]);
            }
            Assert.AreEqual(_orders, actual);
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        public void GetOrderTest(int orderNumber, int indexOfOrder)
        {
            // act
            var result = _repo.GetOrder(orderNumber, new DateTime(2013, 6, 1));
            var actual = _repo.ToCSVForTesting(result);

            // assert
            Assert.AreEqual(_orders[indexOfOrder], actual);
        }

        [Test]
        public void UpdateOrderTest()
        {
            // arrange
            var order = new Order();
            order = _repo.LoadFromCSVForTesting(_orderToUpdate);

            // act
            _repo.UpdateOrder(order, new DateTime(2013, 6, 1));
            var response = _repo.GetOrder(4, new DateTime(2013, 6, 1));
            var responseCSV = _repo.ToCSVForTesting(response);

            // assert
            Assert.AreEqual(_orderToUpdate, responseCSV);
        }

        [Test]
        public void OverwriteFileTest()
        {
            // arrange
            List<Order> orders = new List<Order>();

            foreach (var order in _allOrders)
            {
                orders.Add(_repo.LoadFromCSVForTesting(order));
            }

            // act
            _repo.OverwriteFile(orders, new DateTime(2013, 6, 1));

            var response = _repo.GetAllOrders(new DateTime(2013, 6, 1));
            var actual = new string[3];

            for (int i = 0; i < response.Count; i++)
            {
                actual[i] = _repo.ToCSVForTesting(response[i]);
            }

            // assert
            Assert.AreEqual(_allOrders, actual);
        }

        [Test]
        public void WriteNewLineTest()
        {
            // arrange
            var order = new Order();
            order = _repo.LoadFromCSVForTesting(_orderToAdd);

            // act
            _repo.WriteNewLine(order, new DateTime(2013, 6, 1));


            var orders = _repo.GetAllOrders(new DateTime(2013, 6, 1));

            if (projectMode == "prod")
            {
                var ordersCSV = new string[4];

                for (int i = 0; i < orders.Count; i++)
                {
                    ordersCSV[i] = _repo.ToCSVForTesting(orders[i]);
                }
                // assert
                Assert.AreEqual(_ordersWithNewLineProd, ordersCSV);
            }
            else if (projectMode == "test")
            {
                var ordersCSV = new string[5];

                for (int i = 0; i < orders.Count; i++)
                {
                    ordersCSV[i] = _repo.ToCSVForTesting(orders[i]);
                }
                // assert
                Assert.AreEqual(_ordersWithNewLineTest, ordersCSV);
            }
        }

        [Test]
        public void CheckIfRepoExists()
        {
            bool yay = _repo.CheckIfRepositoryExists(new DateTime(2013, 06, 01));

            Assert.AreEqual(true, yay);
        }

        [Test]
        public void CheckIfRepoExistsTestValidInput()
        {
            var actual = _repo.CheckIfRepositoryExists(new DateTime(2013, 6, 1));

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void CheckIfRepoExistsTestInvalidInput()
        {
            var actual = _repo.CheckIfRepositoryExists(new DateTime(1001, 1, 1));

            if (projectMode == "test")
            {
                Assert.AreEqual(true, actual);
            }
            else if (projectMode == "prod")
            {
                Assert.AreEqual(false, actual);
            }
        }
    }
}
