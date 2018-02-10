using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Kingston.Demo.Ver1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Kingston.Demo.Ver1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Demo.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "面試者:洪健航";

            return View();
        }
        public IActionResult Login()
        {
            ViewData["Message"] = "Your Login page.";
            

            return View();
        }

        [HttpPost]
        public  IActionResult Login(String user, String pwd)
        {
            ViewData["Message"] = "Your Login page.";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
