using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Data.States;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;
using NUnit.Framework;

namespace FlooringApp.Tests
{
    [TestFixture]
    public class StateRepositoryTests
    {
        private IStateRepository _repo;
        private string[] _states;

        [SetUp]
        public void SetUp()
        {
            _repo = StatesRepositoryFactory.CreateStatesRepository();
            _states = new []
            {
            "OH,Ohio,6.25",
            "PA,Pennsylvania,6.75",
            "MI,Michigan,5.75",
            "IN,Indiana,6.00"
            };
        }

        [Test]
        public void GetAllStatesTest()
        {
            var result = _repo.GetAllStates();
            var actual = new string[4];

            for (int i = 0; i < result.Count; i++)
            {
                actual[i] = _repo.ToCSVForTesting(result[i]);
            }
            Assert.AreEqual(_states, actual);
        }

        [TestCase("OH", 0)]
        [TestCase("PA", 1)]
        [TestCase("MI", 2)]
        [TestCase("IN", 3)]
        public void GetStateTest(string stateAbbrev, int indexOfState)
        {
            // arrange
            State state = new State();
            state.StateAbbreviation = stateAbbrev;

            // act
            var result = _repo.GetState(state);
            var actual = _repo.ToCSVForTesting(result);

            // assert
            Assert.AreEqual(_states[indexOfState], actual);

        }

    }
}
