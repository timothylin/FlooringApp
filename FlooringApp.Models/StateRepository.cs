using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Models
{
    public abstract class StateRepository : IStateRepository
    {
        protected List<State> States { get; set; } = new List<State>();

        protected string FilePath = @"DataFiles\Taxes.txt";

        public virtual List<State> GetAllStates()
        {
            try
            {
                var reader = File.ReadAllLines(FilePath);

                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');

                    var state = new State();

                    state.StateAbbreviation = columns[0];
                    state.StateName = columns[1];
                    state.TaxRate = decimal.Parse(columns[2]);

                    States.Add(state);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "State Repo - GetAllStates");
            }

            return States;

        }

        public virtual State GetState(State state)
        {
            var stateToReturn = new State();

            try
            {
                List<State> states = GetAllStates();

                stateToReturn = states.FirstOrDefault(s => s.StateAbbreviation.ToUpper() == state.StateAbbreviation.ToUpper());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "State Repo - GetState");
            }

            return stateToReturn;
        }

        public string ToCSVForTesting(State state)
        {
            return String.Format("{0},{1},{2}", state.StateAbbreviation, state.StateName, state.TaxRate);

        }

        public State LoadFromCSVForTesting(string stateCSV)
        {
            var columns = stateCSV.Split(',');

            var state = new State();

            state.StateAbbreviation = columns[0];
            state.StateName = columns[1];
            state.TaxRate = decimal.Parse(columns[2]);

            return state;
        }
    }
}
