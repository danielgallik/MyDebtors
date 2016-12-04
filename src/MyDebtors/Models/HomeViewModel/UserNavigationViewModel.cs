using System.ComponentModel.DataAnnotations;

namespace MyDebtors.Models.HomeViewModel
{
    public class UserNavigationViewModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Amount { get; set; }
    }
}
