using EstateApplication.Data.DatabaseContext.ApplicationDbContext;
using EstateApplication.Data.Entities;
using EstateApplication.Web.Interfaces;
using EstateApplication.Web.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateApplication.Web.Services
{
    public class PropertiesServices : IPropertiesService
    {
        private readonly ApplicationDbContext _dbcontext;
        public PropertiesServices(ApplicationDbContext context)
        {
            _dbcontext = context;
        }
        public async Task AddProperty(PropertiesModel model)
        {
            var property = new Property
            {
                ID = Guid.NewGuid().ToString(),
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Description = model.Description,
                NumberOfBaths = model.NumberOfBaths,
                NumberOfToilets = model.NumberOfToilets,
                Address = model.Address,
                ContantPhoneNumber = model.ContactPhoneNumber,
                NumberOfRooms = model.NumberOfRooms,
            };

           await _dbcontext.AddAsync(property);
           await _dbcontext.SaveChangesAsync();  
        }
    }
}
