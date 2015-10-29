using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Models;

namespace FlooringApp.UI
{
    public class UserPrompts : ValidateUserInputs
    {
        private OrderSlip _orderSlip;
        private RepositoryOperations _repositoryOperations;

        public UserPrompts() : base()
        {
            _orderSlip = Response.Slip;
            _repositoryOperations = new RepositoryOperations();
        }


        public OrderSlip GetNewOrderInfoFromUser()
        {
            Logger.Info("New order prompts", "User prompts - Get New Order Info From User");

            GetOrderDateFromUser();
            GetCustomerNameFromUser();
            GetStateFromUser();
            GetProductTypeFromUser();
            GetAreaFromUser();

            return _orderSlip;
        }

        public OrderSlip GetDateInfoFromUser()
        {
            Logger.Info("Display orders", "User Prompts - Get Date Info");

            GetOrderDateFromUser();
            return _orderSlip;
        }

        public OrderSlip GetOrderInfoFromUser()
        {
            Logger.Info("Edit / Remove order prompts", "User prompts - Get Order Info From User");

            GetOrderDateFromUser();
            GetOrderNumberFromUser();

            return _orderSlip;
        }

        public bool PromptUserToTryAgain()
        {
            Logger.Info("Edit order prompt user to try again", "User prompts - PromptUserToTryAgain");

            do
            {
                Console.WriteLine("Do you want to try again?  (Y)es or (N)o.");
                var userResponse = Console.ReadLine().ToUpper();

                Response = ValidateUserResponse(userResponse);

            } while (!Response.Success);

            return Response.UserResponse;
        }

        public bool AskUserToSave()
        {

            Logger.Info("Edit order prompt user to save", "User prompts - AskUserToSave");

            do
            {
                Console.Write("Do you want to save this data? (Y)es or (N)o: ");
                var userResponse = Console.ReadLine().ToUpper();

                Response = ValidateUserResponse(userResponse);

            } while (!Response.Success);

            return Response.UserResponse;
        }

        public bool RemovalConfirmation()
        {

            Logger.Info("Remove order confirmation prompt", "User prompts - RemovalConfirmation");

            do
            {
                Console.WriteLine("Are you sure you want to remove this order?  (Y)es or (N)o.");
                var userResponse = Console.ReadLine().ToUpper();

                Response = ValidateUserResponse(userResponse);

            } while (!Response.Success);

            return Response.UserResponse;
        }

        private void GetOrderDateFromUser()
        {
            do
            {
                Console.Clear();
                Console.Write("Enter a date: ");
                string dateInput = Console.ReadLine();

                Response = ValidateDate(dateInput);

                Console.WriteLine(Response.Message);

            } while (!Response.Success);

            _orderSlip = Response.Slip;
        }


        private void GetOrderNumberFromUser()
        {
            do
            {
                Console.Write("Enter an Order number: ");
                string orderNoInput = Console.ReadLine();

                Response = ValidateOrderNumber(orderNoInput);

                Console.WriteLine(Response.Message);

            } while (!Response.Success);

            _orderSlip = Response.Slip;
        }

        private void GetCustomerNameFromUser()
        {
            do
            {
                Console.Write("Enter Customer Name: ");
                string customerNameInput = Console.ReadLine();

                Response = ValidateCustomerName(customerNameInput);

                Console.WriteLine(Response.Message);

            } while (!Response.Success);

            _orderSlip = Response.Slip;
        }

        private void GetStateFromUser()
        {
            do
            {
                Console.Write("Enter a 2 letter state code: ");
                string stateInput = Console.ReadLine().ToUpper();

                Response = ValidateState(stateInput);

                if (Response.Success)
                {
                    var repoOpsResponse = _repositoryOperations.CheckIfStateExists(Response.State);

                    if (repoOpsResponse.Success)
                    {
                        _orderSlip.State = repoOpsResponse.State;
                    }
                    else
                    {
                        Response.Success = false;
                        Response.Message = "That is not a state/province/country/territory we do business in...\n";
                        Logger.Warning("Invalid user Input", "UserPrompts - GetStateFromUser");
                    }
                }

                Console.WriteLine(Response.Message);

            } while (!Response.Success);

        }

        private void GetProductTypeFromUser()
        {
            do
            {
                Console.Write("Pick the type of flooring: (C)arpet, (L)aminate, (T)ile, (W)ood: ");
                string productTypeInput = Console.ReadLine().ToUpper();

                Response = ValidateProductType(productTypeInput);

                if (Response.Success)
                {
                    Response.Slip.ProductType = Response.Product;
                }

                Console.WriteLine(Response.Message);

            } while (!Response.Success);

            _orderSlip.ProductType = Response.Product;
        }


        private void GetAreaFromUser()
        {

            do
            {
                Console.Write("Enter area in square foot: ");
                string areaInput = Console.ReadLine().ToUpper();

                Response = ValidateArea(areaInput);

                Console.WriteLine(Response.Message);

            } while (!Response.Success);

            _orderSlip = Response.Slip;
        }
    }
}
