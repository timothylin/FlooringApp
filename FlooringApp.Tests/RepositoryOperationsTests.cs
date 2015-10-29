using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Data.Products;
using FlooringApp.Data.States;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;
using NUnit.Framework;

namespace FlooringApp.Tests
{
    [TestFixture]
    public class RepositoryOperationsTests
    {
        RepositoryOperations ops;

        [SetUp]
        public void SetUp()
        {
            ops = new RepositoryOperations();
        }

        [TestCase("NY", false)]
        [TestCase("PA", true)]
        [TestCase("OH", true)]
        [TestCase("IL", false)]
        public void CheckIfStateExistsTest(string stateAbbrev, bool expected)
        {
            // arrange
            var state = new State();
            state.StateAbbreviation = stateAbbrev;

            // act
            var response = ops.CheckIfStateExists(state);

            // assert
            Assert.AreEqual(expected, response.Success);
        }

        [TestCase("Random", false)]
        [TestCase("Laminate", true)]
        [TestCase("Carpet", true)]
        [TestCase("Foo", false)]
        public void CheckIfProductExistsTest(string productType, bool expected)
        {
            // arrange
            var product = new Product();
            product.ProductType = productType;

            // act
            var response = ops.CheckIfProductExists(product);

            // assert
            Assert.AreEqual(expected, response.Success);
        }
    }
}
