using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Data.States
{
    public class StatesRepositoryTestMode : StateRepository
    {
        public StatesRepositoryTestMode() : base()
        {
            States.AddRange(new List<State>()
            {
                new State()
                {
                    StateAbbreviation = "OH",
                    StateName = "Ohio",
                    TaxRate = 6.25M
                },
                new State()
                {
                    StateAbbreviation = "PA",
                    StateName = "Pennsylvania",
                    TaxRate = 6.75M
                },
                new State()
                {
                    StateAbbreviation = "MI",
                    StateName = "Michigan",
                    TaxRate = 5.75M
                },
                new State()
                {
                    StateAbbreviation = "IN",
                    StateName = "Indiana",
                    TaxRate = 6.00M
                }
            });
        }

        public override List<State> GetAllStates()
        {
            return States;
        }
    }
}
