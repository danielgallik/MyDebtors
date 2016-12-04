using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyDebtors.Models.HomeViewModel
{
    public class NewTransactionViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [Required]
        [Display(Prompt = "Name")]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Prompt = "Amount")]
        public decimal? Amount { get; set; }

        [Display(Prompt = "Comment")]
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
