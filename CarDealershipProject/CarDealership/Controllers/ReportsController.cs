using CarDealership.Data.Factories;
using CarDealership.Models;
using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class ReportsController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            InventoryReportViewModel model = new InventoryReportViewModel();
            var repo = VehiclesRepositoryFactory.GetRepository();
            model.newVehicles = repo.GetInventoryNew();
            model.usedVehicles = repo.GetInventoryUsed();
            
            return View(model);
        }

        public ActionResult Sales()
        {
            SalesReportViewModel model = new SalesReportViewModel();
            SaleReportParameters parameters = new SaleReportParameters();
            var repo = UsersRepositoryFactory.GetRepository();

            model.Users = repo.GetAll();
            model.Parameters = parameters;

            return View(model);
        }
    }
}