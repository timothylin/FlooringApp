using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.Models
{
    public class State

    {
        public string StateAbbreviation { get; set; }
        public string StateName { get; set; }
        public decimal TaxRate { get; set; }

    }
}
