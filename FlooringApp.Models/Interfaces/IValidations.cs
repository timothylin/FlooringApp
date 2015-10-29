using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringApp.Models.Interfaces
{
    public interface IValidations
    {
        Response Response { get; set; }

        Response ValidateDate(string dateInput);
        Response ValidateOrderNumber(string orderNoInput);
        Response ValidateCustomerName(string customerNameInput);
        Response ValidateState(string stateInput);
        Response ValidateProductType(string productTypeInput);
        Response ValidateArea(string areaInput);
        Response ValidateUserResponse(string userInput);
    }
}
