using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringApp.Models.Interfaces
{
    public interface IStateRepository
    {
        List<State> GetAllStates();

        State GetState(State state);

        string ToCSVForTesting(State state);

        State LoadFromCSVForTesting(string stateCSV);
    }
}
