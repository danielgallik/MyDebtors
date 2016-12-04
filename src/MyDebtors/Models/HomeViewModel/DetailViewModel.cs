using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyDebtors.Data;

namespace MyDebtors.Models.HomeViewModel
{
    public class DebtorsViewModel
    {
        public ApplicationUser Debtor { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal TotalBalance { get; set; }

        public IEnumerable<TransactionViewModel> Transactions;
    }
}
