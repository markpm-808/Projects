using CarDealership.Data.Factories;
using CarDealership.Data.Repositories;
using CarDealership.Models;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        

        public ActionResult Vehicles()
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

        public ActionResult AddVehicle()
        {
            AddVehicleViewModel model = new AddVehicleViewModel();

            var repoMake = MakesRepositoryFactory.GetRepository();
            var repoModel = ModelsRepositoryFactory.GetRepository();
            var repoType = VehicleTypesRepositoryFactory.GetRepository();
            var repoBodyStyle = BodyStyleRepositoryFactory.GetRepository();
            var repoColor = ColorsRepositoryFactory.GetRepository();

            model.Makes = repoMake.GetAll();
            model.Models = repoModel.GetAll();
            model.VehicleTypes = repoType.GetAll();
            model.BodyStyles = repoBodyStyle.GetAll();
            model.Colors = repoColor.GetAll();
            model.InteriorColors = repoColor.GetAll();

            return View(model);
        }
        [HttpPost]
        public ActionResult AddVehicle(AddVehicleViewModel model)
        {
            if (model.NewVehicle.TypeName == "1")
            {
                if( Int32.Parse(model.NewVehicle.Mileage) >1000 || Int32.Parse(model.NewVehicle.Mileage) < 0)
                ModelState.AddModelError("", "Mileage must be between 0 and 1000 for NEW vehicles");
            }
            else
            {
                if (Int32.Parse(model.NewVehicle.Mileage) < 1000 )
                    ModelState.AddModelError("", "Mileage must be greater than 1000 for USED vehicles");
            }
            if(model.NewVehicle.SalePrice > model.NewVehicle.MSRP)
            {
                ModelState.AddModelError("", "Sale price cannot be greater than MSRP");
            }
            
            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    model.NewVehicle.PictureFileName = "temp";
                }
                
                if(model.Checkbox == true)
                {
                    model.NewVehicle.Featured = 1;
                }
                else
                {
                    model.NewVehicle.Featured = 2;
                }

                if(model.NewVehicle.TypeName == "1")
                {
                    model.NewVehicle.Mileage = "New";
                }


                var repo = VehiclesRepositoryFactory.GetRepository();
                repo.Insert(model.NewVehicle);

                var savePath = Server.MapPath("~/Images");
                string fileName = "inventory-" + model.NewVehicle.VehicleID;
                string extension = Path.GetExtension(model.ImageUpload.FileName);
                var filePath = Path.Combine(savePath, fileName + extension);
                model.ImageUpload.SaveAs(filePath);



                repo.UpdateFileName(model.NewVehicle.VinNumber, Path.GetFileName(filePath));
                return RedirectToAction("EditVehicle", new {id = model.NewVehicle.VehicleID});

            }
            else
            {
                var repoMake = MakesRepositoryFactory.GetRepository();
                var repoModel = ModelsRepositoryFactory.GetRepository();
                var repoType = VehicleTypesRepositoryFactory.GetRepository();
                var repoBodyStyle = BodyStyleRepositoryFactory.GetRepository();
                var repoColor = ColorsRepositoryFactory.GetRepository();

                model.Makes = repoMake.GetAll();
                model.Models = repoModel.GetAll();
                model.VehicleTypes = repoType.GetAll();
                model.BodyStyles = repoBodyStyle.GetAll();
                model.Colors = repoColor.GetAll();
                model.InteriorColors = repoColor.GetAll();

                return View("AddVehicle", model);
            }
        }


        public ActionResult EditVehicle(int id)
        {
            EditVehicleViewModel model = new EditVehicleViewModel();

            var repoVehicles = VehiclesRepositoryFactory.GetRepository();
            var repoMake = MakesRepositoryFactory.GetRepository();
            var repoModel = ModelsRepositoryFactory.GetRepository();
            var repoType = VehicleTypesRepositoryFactory.GetRepository();
            var repoBodyStyle = BodyStyleRepositoryFactory.GetRepository();
            var repoColor = ColorsRepositoryFactory.GetRepository();

            model.Makes = repoMake.GetAll();
            model.Models = repoModel.GetAll();
            model.VehicleTypes = repoType.GetAll();
            model.BodyStyles = repoBodyStyle.GetAll();
            model.Colors = repoColor.GetAll();
            model.InteriorColors = repoColor.GetAll();

            model.NewVehicle = repoVehicles.EditGetByID(id);
            model.NewVehicle.BodyStyle = repoVehicles.GetBodyStyle(model.NewVehicle.BodyStyle).ToString();
            model.NewVehicle.Make = repoVehicles.GetMake(model.NewVehicle.Make).ToString();
            model.NewVehicle.Model = repoVehicles.GetModel(model.NewVehicle.Model).ToString();
            model.NewVehicle.Color = repoVehicles.GetColor(model.NewVehicle.Color).ToString();

            if(model.NewVehicle.Mileage == "0" || model.NewVehicle.Mileage == "New")
            {
                model.Type = 1;
            }
            else
            {
                model.Type = 2;
            }

            if (model.NewVehicle.Featured == 1)
            {
                model.Checkbox = true;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicle(EditVehicleViewModel model)
        {
            if(model.NewVehicle.Type == null && model.NewVehicle.Mileage == "New")
            {
                model.NewVehicle.Type = "1";
                model.NewVehicle.Mileage = "0";

            }

            if (model.NewVehicle.Type == "1")
            {
                if (Int32.Parse(model.NewVehicle.Mileage) > 1000 || Int32.Parse(model.NewVehicle.Mileage) < 0)
                    ModelState.AddModelError("", "Mileage must be between 0 and 1000 for NEW vehicles");
            }
            else
            {
                if (Int32.Parse(model.NewVehicle.Mileage) < 1000)
                    ModelState.AddModelError("", "Mileage must be greater than 1000 for USED vehicles");
            }
            if (model.NewVehicle.SalePrice > model.NewVehicle.MSRP)
            {
                ModelState.AddModelError("", "Sale price cannot be greater than MSRP");
            }
            
            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    model.NewVehicle.PictureFileName = "temp";

                }

                if (model.Checkbox == true)
                {
                    model.NewVehicle.Featured = 1;
                }
                else
                {
                    model.NewVehicle.Featured = 2;
                }
                
                if(model.Type == 1)
                {
                    model.NewVehicle.Mileage = "New";
                    model.NewVehicle.Type = "1";
                }
                else
                {
                    model.NewVehicle.Type = "2";
                }


                var repo = VehiclesRepositoryFactory.GetRepository();
                Vehicle vehicle = new Vehicle();
                vehicle.BodyStyleName = model.NewVehicle.BodyStyle;
                vehicle.ColorName = model.NewVehicle.Color;
                vehicle.Description = model.NewVehicle.Description;
                vehicle.Featured = model.NewVehicle.Featured;
                vehicle.Interior = model.NewVehicle.Interior;
                vehicle.MakeName = model.NewVehicle.Make;
                vehicle.Mileage = model.NewVehicle.Mileage;
                vehicle.ModelName = model.NewVehicle.Model;
                vehicle.MSRP = model.NewVehicle.MSRP;
                vehicle.PictureFileName = model.NewVehicle.PictureFileName;
                vehicle.SalePrice = model.NewVehicle.SalePrice;
                vehicle.Transmission = model.NewVehicle.Trans;
                vehicle.TypeName = model.NewVehicle.Type;
                vehicle.VehicleID = model.NewVehicle.VehicleID;
                vehicle.VinNumber = model.NewVehicle.VinNumber;
                vehicle.Year = model.NewVehicle.Year;

                repo.Update(vehicle);

                if(vehicle.PictureFileName == "temp")
                {
                    var savePath = Server.MapPath("~/Images");
                    string fileName = "inventory-" + vehicle.VehicleID;
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    var filePath = Path.Combine(savePath, fileName + extension);
                    model.ImageUpload.SaveAs(filePath);
                    repo.UpdateFileName(vehicle.VinNumber, Path.GetFileName(filePath));
                }
      

                return RedirectToAction("Vehicles");

            }
            else
            {
                var repoMake = MakesRepositoryFactory.GetRepository();
                var repoModel = ModelsRepositoryFactory.GetRepository();
                var repoType = VehicleTypesRepositoryFactory.GetRepository();
                var repoBodyStyle = BodyStyleRepositoryFactory.GetRepository();
                var repoColor = ColorsRepositoryFactory.GetRepository();

                model.Makes = repoMake.GetAll();
                model.Models = repoModel.GetAll();
                model.VehicleTypes = repoType.GetAll();
                model.BodyStyles = repoBodyStyle.GetAll();
                model.Colors = repoColor.GetAll();
                model.InteriorColors = repoColor.GetAll();

                return View("EditVehicle", model);
            }
        }



        public ActionResult Specials()
        {
            SpecialsAddViewModel model = new SpecialsAddViewModel();
            var repo = SpecialsRepositoryFactory.GetRepository();
            model.Specials = repo.GetAll();

            model.AddSpecial = new Special();

            return View(model);
        }

        [HttpPost]
        public ActionResult Specials(SpecialsAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = SpecialsRepositoryFactory.GetRepository();

                repo.AddSpecial(model.AddSpecial);
                return RedirectToAction("Specials");

            }
            else
            {
                var repo = SpecialsRepositoryFactory.GetRepository();
                model.Specials = repo.GetAll();

                model.AddSpecial = new Special();

                return View("Specials", model);
            }
        }

        public ActionResult Users()
        {
            var repo = UsersRepositoryFactory.GetRepository();
            var model = repo.GetAll();

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult AddUser()
        {
            return View();
        }
        


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    RoleID = model.RoleID,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Adds new user to AspNetUserRoles
                    var repo = UsersRepositoryFactory.GetRepository();
                    var id = repo.GetID(model.Email);
                    repo.InsertRole(id, model.RoleID);
          
                    return RedirectToAction("Users", "Admin");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult EditUser(string id)
        {
            var repoUser = UsersRepositoryFactory.GetRepository();
            EditUserViewModel model = new EditUserViewModel();

            var user = repoUser.GetUser(id);
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.RoleID = user.RoleID;
            model.UserID = user.UserID;


            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = UsersRepositoryFactory.GetRepository();
                repo.RemoveUser(model.UserID);

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    RoleID = model.RoleID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Id = model.UserID
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Adds new user to AspNetUserRoles
                    
                    var id = repo.GetID(model.Email);
                    repo.InsertRole(id, model.RoleID);

                    return RedirectToAction("Users", "Admin");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }





        public ActionResult Makes()
        {
            MakesAddViewModel model = new MakesAddViewModel();
            var repo = MakesRepositoryFactory.GetRepository();
            Make Make = new Make();

            model.Makes = repo.GetAll();
            model.NewMake = Make;

            return View(model);
        }

        [HttpPost]
        public ActionResult Makes(MakesAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = MakesRepositoryFactory.GetRepository();
                var userRepo = UsersRepositoryFactory.GetRepository();

                var userEmail = User.Identity.Name;
                model.NewMake.UserEmail = userEmail;

                repo.Insert(model.NewMake);
                return RedirectToAction("Makes");

            }
            else
            {
                var repo = MakesRepositoryFactory.GetRepository();
                Make Make = new Make();

                model.Makes = repo.GetAll();
                model.NewMake = Make;

                return View("Makes", model);
            }
        }

        public ActionResult Models()
        {
            ModelsAddViewModel model = new ModelsAddViewModel();
            var repo = ModelsRepositoryFactory.GetRepository();
            var makeRepo = MakesRepositoryFactory.GetRepository();
            Model addModel = new Model();

            model.Makes = makeRepo.GetAll();
            model.Models = repo.GetAll();
            model.NewModel = addModel;

            return View(model);
        }

        [HttpPost]
        public ActionResult Models(ModelsAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = ModelsRepositoryFactory.GetRepository();
                var userRepo = UsersRepositoryFactory.GetRepository();

                var userEmail = User.Identity.Name;
                model.NewModel.UserEmail = userEmail;

                repo.Insert(model.NewModel);
                return RedirectToAction("Models");

            }
            else
            {
                var repo = ModelsRepositoryFactory.GetRepository();
                Model newModel = new Model();

                model.Models = repo.GetAll();
                model.NewModel = newModel;

                return View("Models", model);
            }
        }


    }
}