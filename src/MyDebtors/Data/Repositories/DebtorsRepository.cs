using System;
using System.Collections.Generic;
using System.Linq;
using MyDebtors.Data.Repositories.Interfaces;
using MyDebtors.Models.HomeViewModel;

namespace MyDebtors.Data.Repositories
{
    public class DebtorsRepository : IDebtorsRepository
    {
        private readonly ApplicationDbContext _db;

        public DebtorsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationUser GetUserById(string id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetOrCreateDebtor(string userId, string debtorName)
        {
            var debtors = _db.Transactions.Where(x => x.Sender.Id == userId || x.Receiver.Id == userId)
                .Select(x => x.Sender.Id != userId ? x.Sender : x.Receiver)
                .GroupBy(x => x.Id)
                .Select(x => x.First());
            foreach (var item in debtors)
            {
                if (item.Name.Equals(debtorName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return item;
                }
            }

            var debtor = new ApplicationUser() { Name = debtorName };
            _db.Users.Add(debtor);
            return debtor;
        }

        public List<TransactionViewModel> GetTransactions(string userId)
        {
            var debts = _db.Transactions.Where(x => x.Receiver.Id == userId).Select(x => new TransactionViewModel()
            {
                Name = x.Sender.Name,
                Amount = -x.Amount,
                Comment = x.Comment,
                Date = x.Date
            });
            var payments = _db.Transactions.Where(x => x.Sender.Id == userId).Select(x => new TransactionViewModel()
            {
                Name = x.Receiver.Name,
                Amount = x.Amount,
                Comment = x.Comment,
                Date = x.Date
            });
            var result = new List<TransactionViewModel>();
            result.AddRange(debts);
            result.AddRange(payments);
            return result.OrderByDescending(x => x.Date).ToList();
        }

        public List<TransactionViewModel> GetTransactions(string primaryUserId, string secoundaryUserId)
        {
            var debts = _db.Transactions.Where(x => x.Receiver.Id == primaryUserId && x.Sender.Id == secoundaryUserId).Select(x => new TransactionViewModel()
            {
                Amount = -x.Amount,
                Comment = x.Comment,
                Date = x.Date
            });
            var payments = _db.Transactions.Where(x => x.Sender.Id == primaryUserId && x.Receiver.Id == secoundaryUserId).Select(x => new TransactionViewModel()
            {
                Amount = x.Amount,
                Comment = x.Comment,
                Date = x.Date
            });
            var result = new List<TransactionViewModel>();
            result.AddRange(debts);
            result.AddRange(payments);
            return result.OrderByDescending(x => x.Date).ToList();
        }

        public List<UserNavigationViewModel> GetDebtors(string userId)
        {
            var debts = _db.Transactions.Where(x => x.Receiver.Id == userId).Select(x => new UserNavigationViewModel()
            {
                Id = x.Sender.Id,
                Name = x.Sender.Name,
                Amount = -x.Amount
            });
            var payments = _db.Transactions.Where(x => x.Sender.Id == userId).Select(x => new UserNavigationViewModel()
            {
                Id = x.Receiver.Id,
                Name = x.Receiver.Name,
                Amount = x.Amount
            });
            var union = new List<UserNavigationViewModel>();
            union.AddRange(debts);
            union.AddRange(payments);

            if (union.Count == 0)
            {
                return union;
            }

            var result = union.GroupBy(x => new { x.Id, x.Name }, x => x.Amount, (key, value) => new UserNavigationViewModel()
            {
                Id = key.Id,
                Name = key.Name,
                Amount = value.Sum()
            });
            return result.ToList();
        }

        public void AddTransaction(string senderId, string receiverId, decimal amount, string comment)
        {
            var transaction = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Sender = new ApplicationUser() { Id = senderId },
                Receiver = new ApplicationUser() { Id = receiverId },
                Amount = amount,
                Comment = comment,
                Date = DateTime.Now
            };

            _db.Transactions.Add(transaction);
            _db.SaveChanges();
        }
    }
}