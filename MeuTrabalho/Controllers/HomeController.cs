using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeuTrabalho.Models;
using System.Data.SqlClient;
using MeuTrabalho.Repository;

namespace MeuTrabalho.Controllers
{
    public class HomeController : Controller, IDisposable
    {
        private ILogRepository _logRepo;

        public HomeController(ILogRepository logRepository)
        {
            _logRepo = logRepository;
        }

        public IActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Account");
        }

        public IActionResult Dashboard(string name)
        {
            if( name == null )
            {
                throw new ArgumentNullException(name);
            }

            ViewBag.Name = name;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            try
            {
                _logRepo.Log("About", "INFO", DateTime.Now);
            }
            catch(Exception ex)
            {
                _logRepo.Log(ex.ToString(), "Error", DateTime.Now);
                ViewData["Message"] = "ERROR ABOUT";
            }

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            try
            {
                _logRepo.Log("Contact", "INFO", DateTime.Now);
            }
            catch(Exception ex)
            {
                _logRepo.Log(ex.ToString(), "Error", DateTime.Now);
                return RedirectToAction("Error");
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
