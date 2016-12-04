using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDebtors.Data;
using MyDebtors.Data.Repositories.Interfaces;
using MyDebtors.Models.HomeViewModel;

namespace MyDebtors.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDebtorsRepository _repository;

        public HomeController(IDebtorsRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Debtors(string debtorId)
        {
            var currentUserId = GetCurrentUserId();
            ViewData["UserList"] = _repository.GetDebtors(currentUserId);
            ViewData["DebtorId"] = debtorId;

            var user = _repository.GetUserById(debtorId);
            var model = new DebtorsViewModel() { Debtor = user };
            List<TransactionViewModel> transactions = null;

            if (user == null)
            {
                if (debtorId != null)
                {
                    ModelState.AddModelError("DebtorId", "Debtor not found.");
                }
                transactions = _repository.GetTransactions(currentUserId);
            }
            else
            {
                transactions = _repository.GetTransactions(currentUserId, debtorId);
            }

            model.TotalBalance = transactions.Sum(x => x.Amount);
            model.Transactions = transactions;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Debtors(NewTransactionViewModel model, string debtorId)
        {
            if (!ModelState.IsValid)
            {
                return Debtors(debtorId);
            }
            var userId = GetCurrentUserId();

            model.Id = _repository.GetOrCreateDebtor(userId, model.Name).Id;

            if (model.TransactionType == TransactionType.Paymant)
            {
                _repository.AddTransaction(userId, model.Id, model.Amount.Value, model.Comment);
            }
            else
            {
                _repository.AddTransaction(model.Id, userId, model.Amount.Value, model.Comment);
            }

            return RedirectToRoute("debtors", new { debtorId = model.Id });
        }

        private string GetCurrentUserId()
        {
            return User?.Claims?.FirstOrDefault()?.Value;
        }
    }
}
