using CarDealership.Data;
using CarDealership.Data.Factories;
using CarDealership.Data.Repositories;
using CarDealership.Models;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarDealership.Controllers
{
    [Authorize(Roles ="Sales")]
    public class SalesController : Controller
    {

        public ActionResult Index()
        {
            var repo = new MakesRepository();
            var list = repo.GetAll();
            string[] makeArray = new string[list.Count()];

            for (int i = 0; i < list.Count(); i++)
            {
                makeArray[i] = list[i].MakeName;
            }

            ViewBag.Makes = makeArray;

            return View(new SearchVehicleParameters());
        }


        public ActionResult Purchase(int id)
        {
            var model = new PurchaseVehicleViewModel();

            var repo = VehiclesRepositoryFactory.GetRepository();
            model.Details = repo.GetDetails(id);

            var stateRepo = StatesRepositoryFactory.GetRepository();
            model.States = stateRepo.GetAll();

            var typeRepo = PurchaseTypesRepositoryFactory.GetRepository();
            model.Types = typeRepo.GetAll();

            ViewBag.VehicleID = model.Details.VehicleID;

            Sale newSale = new Sale();
            model.Sale = newSale;

            return View(model);
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseVehicleViewModel model)
        {
            if(ModelState.IsValidField("Sale.PurchasePrice"))
            {
                var percent = 0.95;
                if (model.Sale.PurchasePrice < (decimal)percent * model.Details.SalePrice)
                {
                    ModelState.AddModelError("", "Purchase price cannot be less than 95 % of the sales price");
                }
                if (model.Sale.PurchasePrice > model.Details.MSRP)
                {
                    ModelState.AddModelError("", "Purchase price cannot exceed the MSRP");
                }
            }
            if (string.IsNullOrEmpty(model.Sale.PhoneNumber) && string.IsNullOrEmpty(model.Sale.Email))
            {
                ModelState.AddModelError("", "Please enter a Phone number or Email address");
            }
         


            if (ModelState.IsValid)
            {
                var db = Settings.GetConnectionString();
                var userRepo = UsersRepositoryFactory.GetRepository();
                var repo = SalesRepositoryFactory.GetRepository();

              
                var userEmail = User.Identity.Name;
                model.Sale.UserID = userRepo.GetID(userEmail);

                repo.Insert(model.Sale);
                return RedirectToAction("Index");

            }
            else
            {
                var repo = VehiclesRepositoryFactory.GetRepository();
                model.Details = repo.GetDetails(model.Sale.VehicleID);

                var stateRepo = StatesRepositoryFactory.GetRepository();
                model.States = stateRepo.GetAll();

                var typeRepo = PurchaseTypesRepositoryFactory.GetRepository();
                model.Types = typeRepo.GetAll();

                ViewBag.VehicleID = model.Details.VehicleID;


                return View("Purchase", model);
            }
        }

    }
}