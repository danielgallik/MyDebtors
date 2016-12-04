using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDebtors.Models.HomeViewModel
{
    public class TransactionViewModel
    {
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Amount { get; set; }

        public string Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
