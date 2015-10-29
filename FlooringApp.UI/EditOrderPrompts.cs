using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.BLL;
using FlooringApp.Models;

namespace FlooringApp.UI
{
    public class EditOrderPrompts : ValidateUserInputs
    {
        private bool _isValidInput;
        private OrderOperations _orderOperations;
        private RepositoryOperations _repositoryOperations;

        public EditOrderPrompts(Order order) : base()
        {
            this._isValidInput = false;
            this.Response.Order = order;
            Response.Updated = false;
            this._orderOperations = new OrderOperations();
            this._repositoryOperations = new RepositoryOperations();
        }

        public Response EditOrder()
        {

            Logger.Info("Edit order prompts", "Edit Order Prompts - Edit Order");

            Console.Clear();
            GetCustomerNameFromUser();
            GetStateFromUser();
            GetProductTypeFromUser();
            GetAreaFromUser();
            return Response;
        }

        private void GetCustomerNameFromUser()
        {
            do
            {
                Console.WriteLine("Current Customer Name ({0}).", Response.Order.CustomerName);
                Console.Write("Edit or leave blank and press enter to leave as-is: ");
                string customerNameInput = Console.ReadLine();

                if (customerNameInput == "")
                {
                    _isValidInput = true;
                }
                else
                {
                    Response = ValidateCustomerName(customerNameInput);

                    if (Response.Success)
                    {
                        Response.Order.CustomerName = customerNameInput;
                        Response.Updated = true;
                        _isValidInput = true;
                    }
                }

                Console.WriteLine(Response.Message);

            } while (!_isValidInput);

            _isValidInput = false;
        }

        private void GetStateFromUser()
        {
            var stateResponse = new Response();

            do
            {
                Console.WriteLine("Type the state abbreviation (MI), (OH), (IN) or (PA).");
                Console.Write("Current State ({0}). Edit or leave blank and press enter to leave as-is: ", Response.Order.State.StateAbbreviation);
                string stateInput = Console.ReadLine().ToUpper();

                if (stateInput == "")
                {
                    _isValidInput = true;
                }
                else
                {
                    stateResponse = ValidateState(stateInput);

                    if (stateResponse.Success)
                    {
                        var repoOpsResponse = _repositoryOperations.CheckIfStateExists(stateResponse.State);

                        if (repoOpsResponse.Success)
                        {
                            Response.Order.State = repoOpsResponse.State;
                            Response.Updated = true;
                            _isValidInput = true;
                        }
                        else
                        {
                            Response.Success = false;
                            Response.Message = "That is not a state/province/country/territory we do business in...\n";
                        }
                    }
                }

                Console.WriteLine(Response.Message);

            } while (!_isValidInput);

            _isValidInput = false;
        }

        private void GetProductTypeFromUser()
        {
            var productResponse = new Response();
            do
            {
                Console.WriteLine("Enter Product Type - Choose from (C)arpet, (L)aminate, (T)ile or (W)ood.");
                Console.Write("Current Product Type - {0}. Edit or leave blank and press enter to leave as-is: ", Response.Order.ProductType.ProductType);
                string productTypeInput = Console.ReadLine().ToUpper();

                if (productTypeInput == "")
                {
                    _isValidInput = true;
                }
                else
                {
                    productResponse = ValidateProductType(productTypeInput);

                    if (Response.Success)
                    {
                        Response.Order.ProductType = productResponse.Product;
                        Response.Updated = true;
                        _isValidInput = true;
                    }
                }

                Console.WriteLine(Response.Message);

            } while (!_isValidInput);

            _isValidInput = false;
        }

        private void GetAreaFromUser()
        {
            do
            {
                Console.Write("Current Area - {0}. Edit or leave blank and press enter to leave as-is: ", Response.Order.Area);
                string areaInput = Console.ReadLine().ToUpper();

                if (areaInput == "")
                {
                    _isValidInput = true;
                }
                else
                {
                    Response = ValidateArea(areaInput);

                    if (Response.Success)
                    {
                        Response.Order.Area = Response.Slip.Area;
                        Response.Updated = true;
                        _isValidInput = true;
                    }
                }

                Console.WriteLine(Response.Message);

            } while (!_isValidInput);

            _isValidInput = false;
        }
    }
}
