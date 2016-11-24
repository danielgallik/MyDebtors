using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDebtors.Models.HomeViewModel
{
    public class IndexViewModel
    {
        public decimal TotalBalance { get; set; }

        public IEnumerable<TransactionViewModel> Transactions;
    }
}
