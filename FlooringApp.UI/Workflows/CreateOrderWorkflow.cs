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
    public class CreateOrderWorkflow : IWorkflow
    {
        public void Execute()
        {
            var prompts = new UserPrompts();
            var ops = new OrderOperations();

            var orderToCreate = prompts.GetNewOrderInfoFromUser();
            var response = ops.CreateOrder(orderToCreate);

            DisplayOrderInformation.DisplayNewOrderInfo(response.Order);

            bool isSaving = prompts.AskUserToSave();

            if (isSaving)
            {
                response.Slip = orderToCreate;
                ops.WriteNewOrderToRepository(response);
                DisplayOrderInformation.DisplayNewOrderConfirmation(response.Order);
            }
            else
            {
                Console.WriteLine("Returning to Main Menu.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
