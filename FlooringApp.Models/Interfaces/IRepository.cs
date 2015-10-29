using FlooringApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringApp.Models.Interfaces
{
    public interface IRepository<T> where T : IWritable
    {
        // use generic repository for product and state (and maybe order)
        // http://blog.falafel.com/implement-step-step-generic-repository-pattern-c/

        // implement IWritable interface for all items (List<T> : Where IWritable)
        // make generic repo containing
        // write toCSV method (add commas) return string
        // write LoadCSV method (parse string with commas) void
        // for unit testing

        // displays all orders
        List<T> GetAllItems(DateTime date);

        // get specific order
        T GetItem(T item, DateTime date);

        // check if repo exists
        bool CheckIfRepositoryExists(T item, DateTime date);

        // edits order
        void UpdateItem(T item, DateTime date);

        // edit/remove order
        void OverwriteFile(List<T> items, DateTime date);

        // add an order
        void WriteNewLine(T item, DateTime date);


    }
}
