using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateApplication.Web.Interfaces;
using EstateApplication.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstateApplication.Web.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertiesService _propertiesService;

        public PropertiesController(IPropertiesService propertiesService)
        {
            _propertiesService = propertiesService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Add(PropertiesModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _propertiesService.AddProperty(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Entry was not saved please try again", e.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}