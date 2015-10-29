using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.UI.Workflows
{
    public class RemoveOrderWorkflow : IWorkflow
    {
        public void Execute()
        {
            var prompts = new UserPrompts();
            var orderSlipFromUser = prompts.GetOrderInfoFromUser();
            var ops = new OrderOperations();

            bool goAgain = false;

            do
            {
                var orderResponse = ops.GetOrder(orderSlipFromUser.OrderNumber, orderSlipFromUser.Date);

                if (!orderResponse.Success)
                {
                    Console.WriteLine(orderResponse.Message);
                    Console.WriteLine("Do you want to try again?");
                    string tryAgainOrNot = Console.ReadLine().ToUpper();
                    Console.Clear();

                    switch (tryAgainOrNot)
                    {
                        case "Y":
                            goAgain = true;
                            break;
                        case "N":
                            goAgain = false;
                            break;
                        default:
                            Console.WriteLine("That was not a valid input.");
                            break;
                    }
                }
                else
                {
                    DisplayOrderInformation.DisplayRemoveOrderInfo(orderResponse.Order);

                    var confirmation = prompts.RemovalConfirmation();

                    if (confirmation)
                    {
                        ops.RemoveOrder(orderSlipFromUser.OrderNumber, orderSlipFromUser.Date);
                        Console.WriteLine();
                        Console.WriteLine("Order removed. press any key to return to Main menu.");
                        Console.ReadKey();
                        goAgain = false;
                    }
                }
            } while (goAgain);
        }
    }
}
