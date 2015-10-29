using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.UI.Workflows
{
    public class DisplayOrderWorkflow : IWorkflow
    {
        public void Execute()
        {
            var prompts = new UserPrompts();
            var orderQuery = prompts.GetDateInfoFromUser();
            DisplayOrderInformation.DisplayRepoInfo(orderQuery);
        }
    }
}
