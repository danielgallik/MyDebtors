using System;
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
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return View((IndexViewModel)null);
            }
            ViewData["UserList"] = _repository.GetDebtors(currentUserId);

            var transactions = _repository.GetTransactions(currentUserId);
            var model = new IndexViewModel()
            {
                TotalBalance = transactions.Sum(x => x.Amount),
                Transactions = transactions
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(string userId)
        {
            var user = _repository.GetUserById(userId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Debtor not found.");
                return RedirectToAction("Index");
            }
            var currentUserId = GetCurrentUserId();

            ViewData["UserList"] = _repository.GetDebtors(currentUserId);
            ViewData["UserId"] = userId;

            var transactions = _repository.GetTransactions(currentUserId, userId);
            var model = new DetailViewModel()
            {
                Id = user.Id,
                UserName = user.Name,
                TotalBalance = transactions.Sum(x => x.Amount),
                Transactions = transactions
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTransaction(NewTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
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

            return RedirectToRoute("detail", new { userId = model.Id });
        }

        private string GetCurrentUserId()
        {
            return User?.Claims?.FirstOrDefault()?.Value;
        }
    }
}
