using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using CarDealership.Data.Factories;
using CarDealership.Data.Repositories;
using CarDealership.Models.Queries;

namespace CarDealership.Controllers
{
    public class InventoryController : Controller
    {
        public ActionResult New()
        {
            var repo = new MakesRepository();
            var list = repo.GetAll();
            string[] makeArray = new string[list.Count()];

          for(int i = 0; i < list.Count(); i++)
            {
                makeArray[i] = list[i].MakeName;
            }

            ViewBag.Makes = makeArray;

            return View(new SearchVehicleParameters());
        }

        public ActionResult Used()
        {
            var repo = new MakesRepository();
            var list = repo.GetAll();
            string[] makeArray = new string[list.Count()];

            for(int i = 0; i < list.Count(); i++)
            {
                makeArray[i] = list[i].MakeName;
            }
            ViewBag.Makes = makeArray;

            return View(new SearchVehicleParameters());
        }




        public ActionResult Details(int id)
        {
            var repo = VehiclesRepositoryFactory.GetRepository();
            var model = repo.GetDetails(id);

            var repoSales = SalesRepositoryFactory.GetRepository();
            model.Purchased = repoSales.CheckSale(id);

            ViewBag.VinNumber = model.VinNumber;
            return View(model);
        }

     
    }
}
