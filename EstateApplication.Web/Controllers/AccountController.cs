using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateApplication.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstateApplication.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            throw new NotImplementedException();
        }
    }
}