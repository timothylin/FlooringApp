using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.UI.Workflows
{
    public class EditOrderWorkflow : IWorkflow
    {
        public void Execute()
        {
            var prompts = new UserPrompts();
            var ops = new OrderOperations();
            EditOrderPrompts query;
            var response = new Response();
            var slip = new OrderSlip();
            bool tryAgain = false;

            do
            {
                tryAgain = false;
                slip = prompts.GetOrderInfoFromUser();
                response = ops.GetOrder(slip.OrderNumber, slip.Date);
                if (!response.Success)
                {
                    Console.WriteLine(response.Message);
                    tryAgain = prompts.PromptUserToTryAgain();
                }
                else
                {
                    query = new EditOrderPrompts(response.Order);
                    var queryResponse = query.EditOrder();

                    if (queryResponse.Updated)
                    {
                        ops.EditOrder(queryResponse.Order, slip.Date);
                    }

                    DisplayOrderInformation.DisplayEditOrderInfo(queryResponse.Order);
                }
            } while (tryAgain);
        }
    }
}
