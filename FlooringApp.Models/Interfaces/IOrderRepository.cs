using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringApp.Models.Interfaces
{
    public interface IOrderRepository
    {
        // displays all orders
        List<Order> GetAllOrders(DateTime date);

        // get specific order
        Order GetOrder(int orderNumber, DateTime date);

        // check if repo exists
        bool CheckIfRepositoryExists(DateTime date);

        // edits order
        void UpdateOrder(Order orderToUpdate, DateTime date);

        // edit/remove order
        void OverwriteFile(List<Order> orders, DateTime date);

        // add an order
        void WriteNewLine(Order order, DateTime date);

        string ToCSVForTesting(Order order);

        Order LoadFromCSVForTesting(string orderCSV);
    }
}
