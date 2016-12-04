using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDebtors.Data
{
    public class Transaction
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }
        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}