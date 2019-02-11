using MeuTrabalho.Models;
using MeuTrabalho.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MeuTrabalho.Controllers
{
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepo;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepo = accountRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            try
            {
                var username = _accountRepo.Login(model);

                return RedirectToAction("Dashboard", "Home", new { name = username});
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }
    }
}
