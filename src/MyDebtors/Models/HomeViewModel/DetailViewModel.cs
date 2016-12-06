using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public List<object> GetChartData()
        {
            if (!Transactions.Any())
            {
                return new List<object>();
            }
            var result = new List<object>();
            var users = Transactions.GroupBy(x => x.Name);
            foreach (var user in users)
            {
                var username = user.Key;
                var data = new List<object>();

                var today = DateTime.Now.ToString("yyyy-MM-dd");
                var days = user.OrderBy(x => x.Date).GroupBy(x => x.Date.ToString("yyyy-MM-dd"));
                var sum = 0m;
                var addToday = true;
                foreach (var day in days)
                {
                    var key = day.Key;
                    sum += day.Sum(x => x.Amount);
                    data.Add(new { x = key, y = sum });

                    if (today == key)
                    {
                        addToday = false;
                    }
                }
                if (addToday)
                {
                    data.Add(new { x = today, y = sum });
                }
                result.Add(new { label = username, data = data });
            }
            return result;
        }
    }
}
