using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Models;
using FlooringApp.UI.Workflows;

namespace FlooringApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Info("Flooring app started", "Main");
            MainMenu.Execute();
            Logger.Info("Flooring app closed", "Main");
        }
    }
}
