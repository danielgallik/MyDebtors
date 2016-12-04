using System.Collections.Generic;
using MyDebtors.Models.HomeViewModel;

namespace MyDebtors.Data.Repositories.Interfaces
{
    public interface IDebtorsRepository
    {
        ApplicationUser GetUserById(string id);
        ApplicationUser GetOrCreateDebtor(string userId, string debtorName);
        List<TransactionViewModel> GetTransactions(string userId);
        List<TransactionViewModel> GetTransactions(string primaryUserId, string secoundaryUserId);
        List<UserNavigationViewModel> GetDebtors(string userId);
        void AddTransaction(string senderId, string receiverId, decimal amount, string comment);
    }
}