using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyDebtors.Data;
using MyDebtors.Models.JavascriptModels;

namespace MyDebtors.Models.HomeViewModel
{
    public class DebtorsViewModel
    {
        public ApplicationUser Debtor { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal TotalBalance { get; set; }

        public IEnumerable<TransactionViewModel> Transactions;

        public List<Dataset> GetChartData()
        {
            if (!Transactions.Any())
            {
                return new List<Dataset>();
            }
            var random = new Random();

            var result = new List<Dataset>();
            foreach (var user in Transactions.GroupBy(x => x.Name))
            {
                var r = random.Next(256);
                var g = random.Next(256);
                var b = random.Next(256);

                var dataset = new Dataset()
                {
                    label = user.Key,
                    fill = false,
                    backgroundColor = $"rgba({r},{g},{b},0.4)",
                    borderColor = $"rgba({r},{g},{b},1)"
                };
                dataset.GenerateData(user);
                result.Add(dataset);
            }
            if (result.Count() == 1)
            {
                result.First().fill = true;
                return result;
            }

            var overallDataset = new Dataset()
            {
                label = "Overall ballance",
                fill = true
            };
            overallDataset.GenerateData(Transactions);

            result.Add(overallDataset);
            return result;
        }
    }
}
