using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Data.Products;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Data.States
{
    public class StatesRepositoryFactory
    {
        public static IStateRepository CreateStatesRepository()
        {
            var projectMode = ConfigurationManager.AppSettings["mode"];

            switch (projectMode)
            {
                case "prod":
                    return new StatesRepositoryProdMode();
                case "test":
                    return new StatesRepositoryTestMode();
                default:
                    throw new NotSupportedException(String.Format("{0} not supported!  You suck.", projectMode));
            }
        }
    }
}
