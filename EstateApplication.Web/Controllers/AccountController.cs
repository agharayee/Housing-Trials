using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateApplication.Data.Entities;
using EstateApplication.Web.Interfaces;
using EstateApplication.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EstateApplication.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountsServices;
        private readonly SignInManager<ApplicationUser> _siginManager;

        public AccountController(IAccountServices accountsServices, SignInManager<ApplicationUser> siginManager)
        {
            _accountsServices = accountsServices;
            _siginManager = siginManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _siginManager.SignOutAsync();
            return LocalRedirect("~/");
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
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var user = await  _accountsServices.CreateUsersAsync(model);
                await _siginManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect("~/");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }
    }
}