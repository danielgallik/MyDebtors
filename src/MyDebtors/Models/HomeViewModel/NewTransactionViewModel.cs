using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDebtors.Models.HomeViewModel
{
    public class NewTransactionViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public TransactionType TransactionType { get; set; }
    }

    public enum TransactionType
    {
        Debt,
        Paymant
    }
}
