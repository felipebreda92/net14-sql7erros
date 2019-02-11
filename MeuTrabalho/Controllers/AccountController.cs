using MeuTrabalho.Models;
using MeuTrabalho.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MeuTrabalho.Controllers
{
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepo;
        private ILogRepository _log;

        public AccountController(IAccountRepository accountRepository, ILogRepository log)
        {
            _accountRepo = accountRepository;
            _log = log;
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

                _log.Log($"Usuário Logado {username}", "INFO", DateTime.Now);

                return RedirectToAction("Dashboard", "Home", new { name = username});
            }
            catch(Exception ex)
            {
                _log.Log(ex.ToString(), "Erro", DateTime.Now);
                return View(model);
            }
        }
    }
}
