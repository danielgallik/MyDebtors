using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDebtors.Models;
using MyDebtors.Models.HomeViewModel;

namespace MyDebtors.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _singInManager;

        public HomeController(SignInManager<ApplicationUser> singInManager)
        {
            _singInManager = singInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!_singInManager.IsSignedIn(User))
            {
                return View((IndexViewModel)null);
            }

            ViewData["UserList"] = new List<UserNavigationViewModel>(); //TODO: Load users from db (who have transaction with current user)

            var transactions = new List<TransactionViewModel>(); //TODO: Select transactions from db
            var totalBalance = 0m; //TODO: Calculate from all user transactions
            var model = new IndexViewModel()
            {
                TotalBalance = totalBalance,
                Transactions = transactions
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(string userId)
        {
            var user = new ApplicationUser() { UserName = userId }; //TODO: Get user from db by userId  
            // add check if user exist

            ViewData["UserList"] = new List<UserNavigationViewModel>(); //TODO: Load users from db (who have transaction with current user)
            ViewData["UserId"] = user.Id;

            var transactions = new List<TransactionViewModel>(); //TODO: Load transactions from db by user
            var totalBalance = 0m; //TODO: Calculate from all user transactions
            var model = new DetailViewModel()
            {
                UserName = user.UserName,
                TotalBalance = totalBalance,
                Transactions = transactions
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTransaction(NewTransactionViewModel model, string userId)
        {
            if (ModelState.IsValid)
            {
                //TODO: Add new transaction to db and redirect to detail page of transaction user
                return RedirectToAction("Detail", new { userId = model.Name });
            }

            return RedirectToAction("Index");
        }
    }
}
