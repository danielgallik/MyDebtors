using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDebtors.Models.HomeViewModel
{
    public class DetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal TotalBalance { get; set; }

        public IEnumerable<TransactionViewModel> Transactions;
    }
}
