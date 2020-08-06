using EstateApplication.Data.Entities;
using EstateApplication.Web.Interfaces;
using EstateApplication.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateApplication.Web.Services
{
    public class AccountsServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApplicationUser> CreateUsersAsync(RegisterModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(message: "Invalid Details Provided", null);
            }
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName

            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(message: AddError(result), null);
            }
            return user;
        }

        private string AddError(IdentityResult result)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var errors in result.Errors)
            {
                sb.Append(errors.Description + " ");         
            }
            return sb.ToString();
        }
    }
}
