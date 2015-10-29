using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Models
{
    public abstract class ValidateUserInputs : IValidations
    {
        public Response Response { get; set; }

        public ValidateUserInputs()
        {
            Response = new Response();
        }

        public Response ValidateDate(string dateInput)
        {
            DateTime date;
            if (DateTime.TryParse(dateInput, out date))
            {
                Response.Success = true;
                Response.Message = "";
                Response.Slip.Date = date;
            }
            else
            {
                Response.Success = false;
                Response.Message = "That was not a valid input.\n";
                Logger.Warning("Invalid user Input", "ValidateUserInputs - ValidateDate");
            }

            return Response;
        }

        public Response ValidateOrderNumber(string orderNoInput)
        {
            int orderNumber;
            if (int.TryParse(orderNoInput, out orderNumber))
            {
                Response.Success = true;
                Response.Message = "";
                Response.Slip.OrderNumber = orderNumber;
            }
            else
            {
                Response.Success = false;
                Response.Message = "That was not a valid input...\n";
                Logger.Warning("Invalid user Input", "ValidateUserInputs - ValidateOrderNumber");
            }

            return Response;
        }

        public Response ValidateCustomerName(string customerNameInput)
        {
            if (customerNameInput == "")
            {
                Response.Success = false;
                Response.Message = "404 error occurred.  Try again!!\n";
                Logger.Warning("Invalid user Input", "ValidateUserInputs - ValidateCustomerName");
            }
            else
            {
                Response.Success = true;
                Response.Message = "";
                Response.Slip.CustomerName = customerNameInput;
            }

            return Response;
        }

        public Response ValidateState(string stateInput)
        {
            State stateQuery = new State();

            if (stateInput.Length == 2)
            {
                Response.Success = true;
                Response.Message = "";
                stateQuery.StateAbbreviation = stateInput;
                Response.State = stateQuery;
            }
            else
            {
                Response.Success = false;
                Response.Message = "That is not a valid input...\n";
                Logger.Warning("Invalid user Input", "ValidateUserInputs - ValidateState");
            }

            return Response;
        }

        public Response ValidateProductType(string productTypeInput)
        {
            Product productQuery = new Product();

            switch (productTypeInput)
            {
                case "C":
                    productQuery.ProductType = "CARPET";
                    Response.Success = true;
                    Response.Message = "";
                    Response.Product = productQuery;
                    break;
                case "L":
                    productQuery.ProductType = "LAMINATE";
                    Response.Success = true;
                    Response.Message = "";
                    Response.Product = productQuery;
                    break;
                case "T":
                    productQuery.ProductType = "TILE";
                    Response.Success = true;
                    Response.Message = "";
                    Response.Product = productQuery;
                    break;
                case "W":
                    productQuery.ProductType = "WOOD";
                    Response.Success = true;
                    Response.Message = "";
                    Response.Product = productQuery;
                    break;
                default:
                    Response.Success = false;
                    Response.Message = "That was not a valid input. C, L, T, and W are the only valid inputs.\n";
                    Logger.Warning("Invalid user Input", "ValidateUserInputs - ValidateProductType");
                    break;
            }

            return Response;
        }

        public Response ValidateArea(string areaInput)
        {
            decimal area;
            if (decimal.TryParse(areaInput, out area))
            {
                if (area <= 0)
                {
                    Response.Success = false;
                    Response.Message = "You must enter a number greater than 0.\n";
                    Logger.Warning("Invalid user Input (negative number)", "ValidateUserInputs - ValidateArea");
                }
                else
                {
                    Response.Success = true;
                    Response.Message = "";
                    Response.Slip.Area = area;
                }
            }
            else
            {
                Response.Success = false;
                Response.Message = "That was not a valid input...\n";
                Logger.Warning("Invalid user Input", "ValidateUserInputs - ValidateArea");
            }

            return Response;
        }

        public Response ValidateUserResponse(string userInput)
        {
            switch (userInput)
            {
                case "Y":
                    Response.Success = true;
                    Response.UserResponse = true;
                    Response.Message = "";
                    break;
                case "N":
                    Response.Success = true;
                    Response.UserResponse = false;
                    Response.Message = "";
                    break;
                default:
                    Response.Success = false;
                    Response.Message = "That was not a valid answer.";
                    Logger.Warning("Invalid user Input", "ValidateUserInputs - ValidateUserResponse");
                    break;
            }

            return Response;
        }
    }
}
