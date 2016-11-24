using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDebtors.Models.HomeViewModel
{
    public class DetailViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalBalance { get; set; }

        public IEnumerable<TransactionViewModel> Transactions;
    }
}
