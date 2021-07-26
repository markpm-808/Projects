using CarDealership.Data.Factories;
using CarDealership.Data.Repositories;
using CarDealership.Models;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var featuredVehicles = VehiclesRepositoryFactory.GetRepository().GetAllFeaturedVehicles();

            var repo = new SpecialsRepository();
            var specials = repo.GetAll();

            var model = new HomePageViewModel();
            model.FeaturedModel = featuredVehicles;
            model.SpecialsModel = specials;

            return View(model);
        }

        public ActionResult Specials()
        {
            var model = SpecialsRepositoryFactory.GetRepository().GetAll();

            return View(model);
        }

        public ActionResult Contact(string vinNumber)
        {
            ViewBag.VinNumber = vinNumber;
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            if(string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name",
                    "Please enter your name");
            }

            if (string.IsNullOrEmpty(model.Message))
            {
                ModelState.AddModelError("Message",
                    "Please enter a message");
            }

            if (string.IsNullOrEmpty(model.Email) & string.IsNullOrEmpty(model.PhoneNumber))
            {
                ModelState.AddModelError("Email",
                    "Please enter an email address or phone number");
                ModelState.AddModelError("PhoneNumber",
                    "Please enter an email address or phone number");
            }
            
            if (ModelState.IsValid)
            {
                var repo = ContactsRepositoryFactory.GetRepository();

                repo.Insert(model);
                return RedirectToAction("Contact");

            }
            else
            {
                return View("Contact",model);
            }
        }

    }
}