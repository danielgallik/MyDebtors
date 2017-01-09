using System;
using System.Collections.Generic;
using System.Linq;
using MyDebtors.Models.HomeViewModel;

namespace MyDebtors.Models.JavascriptModels
{
    public class Dataset
    {
        public string label { get; set; }
        public List<object> data { get; set; }
        public bool fill { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }

        public void GenerateData(IEnumerable<TransactionViewModel> transactions)
        {
            data = new List<object>();
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var days = transactions.OrderBy(x => x.Date).GroupBy(x => x.Date.ToString("yyyy-MM-dd"));
            var sum = 0m;
            foreach (var day in days)
            {
                var key = day.Key;
                sum += day.Sum(x => x.Amount);
                data.Add(new { x = key, y = sum });
            }
            data.Add(new { x = today, y = sum });
        }
    }
}