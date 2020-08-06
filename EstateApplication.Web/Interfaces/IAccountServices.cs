using EstateApplication.Data.Entities;
using EstateApplication.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateApplication.Web.Interfaces
{
    public interface IAccountServices
    {
        Task<ApplicationUser> CreateUsersAsync(RegisterModel model);
       
    }
}
