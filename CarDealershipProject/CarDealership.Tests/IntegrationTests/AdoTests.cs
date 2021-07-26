using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CarDealership.Data.Repositories;
using CarDealership.Models.Tables;
using CarDealership.Models.Queries;

namespace CarDealership.Tests.IntegrationTests
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
        // Purchase Types Tests
        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new PurchaseTypesRepository();

            var types = repo.GetAll();

            Assert.AreEqual(3, types.Count());

            Assert.AreEqual(1, types[0].PurchaseTypeID);
            Assert.AreEqual("Bank Finance", types[0].PurchaseTypeName);
        }
        // Vehicle Types Tests
        [Test]
        public void CanLoadVehicleTypes()
        {
            var repo = new VehicleTypesRepository();

            var types = repo.GetAll();

            Assert.AreEqual(2, types.Count());

            Assert.AreEqual(1, types[0].TypeID);
            Assert.AreEqual("New", types[0].TypeName);
        }

        [Test]
        public void CanGetVehicleTypesByID()
        {
            var repo = new VehicleTypesRepository();

            var type = repo.GetByID(1);

            Assert.AreEqual("New", type.TypeName);
            type = repo.GetByID(2);
            Assert.AreEqual("Used", type.TypeName);
        }

        // Vehicles
        [Test]
        public void CanGetAllFeaturedVehicles()
        {
            var repo = new VehiclesRepository();
            List<FeaturedVehicle> featured = new List<FeaturedVehicle>();

            featured = repo.GetAllFeaturedVehicles();

            Assert.AreEqual(7, featured.Count());
            Assert.AreEqual(2, featured[0].VehicleID);
            Assert.AreEqual(2020, featured[0].Year);
            Assert.AreEqual("Audi", featured[0].MakeName);
            Assert.AreEqual("A3", featured[0].ModelName);
            Assert.AreEqual(11000, featured[0].SalePrice);
            Assert.AreEqual("inventory-2", featured[0].PictureFileName);
        }
        
        [Test]
        public void CanGetVehicleDetails()
        {
            var repo = new VehiclesRepository();

            var vehicle = repo.GetDetails(2);

            Assert.AreEqual(2, vehicle.VehicleID);
            Assert.AreEqual("JH4DB7560SS004122", vehicle.VinNumber);
            Assert.AreEqual(2020, vehicle.Year);
            Assert.AreEqual("Grey", vehicle.Interior);
            Assert.AreEqual("20000", vehicle.Mileage);
            Assert.AreEqual("Automatic", vehicle.Transmission);
            Assert.AreEqual(12200, vehicle.MSRP);
            Assert.AreEqual(11000, vehicle.SalePrice);
            Assert.AreEqual("Audi A3 Car grey", vehicle.Description);
            Assert.AreEqual("inventory-2", vehicle.PictureFileName);
            Assert.AreEqual("Audi", vehicle.MakeName);
            Assert.AreEqual("A3", vehicle.ModelName);
            Assert.AreEqual("Car", vehicle.BodyStyleName);
            Assert.AreEqual("Grey", vehicle.ColorName);
        }

        [Test]
        public void CanGetVehicleByID()
        {
            var repo = new VehiclesRepository();

            var vehicle = repo.GetByID(10);

            Assert.AreEqual(10, vehicle.VehicleID);
            Assert.AreEqual("1J4FJ78L5KL535075", vehicle.VinNumber);
            Assert.AreEqual(2012, vehicle.Year);
            Assert.AreEqual("Grey", vehicle.Interior);
            Assert.AreEqual("43200", vehicle.Mileage);
            Assert.AreEqual("Automatic", vehicle.Transmission);
            Assert.AreEqual(12200, vehicle.MSRP);
            Assert.AreEqual(12000, vehicle.SalePrice);
            Assert.AreEqual("Toyota Tacoma Silver truck", vehicle.Description);
            Assert.AreEqual("inventory-10", vehicle.PictureFileName);
            Assert.AreEqual(true, vehicle.Featured);
            Assert.AreEqual("Toyota", vehicle.MakeName);
            Assert.AreEqual("Used", vehicle.TypeName);
            Assert.AreEqual("Tacoma", vehicle.ModelName);
            Assert.AreEqual("Truck", vehicle.BodyStyleName);
            Assert.AreEqual("Silver", vehicle.ColorName);
        }

        [Test]
        public void CanDeleteVehicle()
        {
            var repo = new VehiclesRepository();

            repo.Delete(10);
            Assert.AreEqual(null, repo.GetByID(10));
        }

        [Test]
        public void CanAddVehicle()
        {
            var repo = new VehiclesRepository();

            Vehicle newVehicle = new Vehicle();

            newVehicle.VinNumber = "1GCEK14K8RE106083";
            newVehicle.Year = 2019;
            newVehicle.Interior = "Black";
            newVehicle.Mileage = "New";
            newVehicle.Transmission = "Manual";
            newVehicle.MSRP = 42199;
            newVehicle.SalePrice = 41999;
            newVehicle.Description = "Toyota Corolla Silver car";
            newVehicle.PictureFileName = "inventory-13";
            newVehicle.Featured = 0;
            newVehicle.MakeName = "Toyota";
            newVehicle.TypeName = "New";
            newVehicle.ModelName = "Corolla";
            newVehicle.BodyStyleName = "Car";
            newVehicle.ColorName = "White";

            repo.Insert(newVehicle);

            var vehicle = repo.GetByID(newVehicle.VehicleID);

            Assert.AreEqual(13, vehicle.VehicleID);
            Assert.AreEqual("1GCEK14K8RE106083", vehicle.VinNumber);
            Assert.AreEqual(2019, vehicle.Year);
            Assert.AreEqual("Black", vehicle.Interior);
            Assert.AreEqual("New", vehicle.Mileage);
            Assert.AreEqual("Manual", vehicle.Transmission);
            Assert.AreEqual(42199, vehicle.MSRP);
            Assert.AreEqual(41999, vehicle.SalePrice);
            Assert.AreEqual("Toyota Corolla Silver car", vehicle.Description);
            Assert.AreEqual("inventory-13", vehicle.PictureFileName);
            Assert.AreEqual(false, vehicle.Featured);
            Assert.AreEqual("Toyota", vehicle.MakeName);
            Assert.AreEqual("New", vehicle.TypeName);
            Assert.AreEqual("Corolla", vehicle.ModelName);
            Assert.AreEqual("Car", vehicle.BodyStyleName);
            Assert.AreEqual("White", vehicle.ColorName);
        }

        [Test]
        public void CanDoUpdateVehicle()
        {
            var repo = new VehiclesRepository();

            Vehicle toUpdate = repo.GetByID(10);

            toUpdate.VinNumber = "1GCEK14K8RE106083";
            toUpdate.Year = 2019;
            toUpdate.Interior = "Black";
            toUpdate.Mileage = "New";
            toUpdate.Transmission = "Manual";
            toUpdate.MSRP = 42199;
            toUpdate.SalePrice = 42000;
            toUpdate.Description = "Toyota Corolla Silver car";
            toUpdate.PictureFileName = "inventory-test";
            toUpdate.Featured = 0;
            toUpdate.MakeName = "Toyota";
            toUpdate.TypeName = "New";
            toUpdate.ModelName = "Corolla";
            toUpdate.BodyStyleName = "Car";
            toUpdate.ColorName = "Silver";

            repo.Update(toUpdate);

            var vehicle = repo.GetByID(10);

            Assert.AreEqual(10, vehicle.VehicleID);
            Assert.AreEqual("1GCEK14K8RE106083", vehicle.VinNumber);
            Assert.AreEqual(2019, vehicle.Year);
            Assert.AreEqual("Black", vehicle.Interior);
            Assert.AreEqual("New", vehicle.Mileage);
            Assert.AreEqual("Manual", vehicle.Transmission);
            Assert.AreEqual(42199, vehicle.MSRP);
            Assert.AreEqual(42000, vehicle.SalePrice);
            Assert.AreEqual("Toyota Corolla Silver car", vehicle.Description);
            Assert.AreEqual("inventory-test", vehicle.PictureFileName);
            Assert.AreEqual(false, vehicle.Featured);
            Assert.AreEqual("Toyota", vehicle.MakeName);
            Assert.AreEqual("New", vehicle.TypeName);
            Assert.AreEqual("Corolla", vehicle.ModelName);
            Assert.AreEqual("Car", vehicle.BodyStyleName);
            Assert.AreEqual("Silver", vehicle.ColorName);
        }

        [Test]
        public void CanSearchNew()
        {
            var repo = new VehiclesRepository();

            SearchVehicleParameters noParams = new SearchVehicleParameters();
            SearchVehicleParameters makeMinPriceMaxPrice = new SearchVehicleParameters();
            SearchVehicleParameters modelMinYearMaxYear = new SearchVehicleParameters();
            SearchVehicleParameters yearMinPriceMaxPrice = new SearchVehicleParameters();

            List<SearchVehicle> vehicles = new List<SearchVehicle>();
            List<SearchVehicle> vehicles2 = new List<SearchVehicle>();
            List<SearchVehicle> vehicles3 = new List<SearchVehicle>();
            List<SearchVehicle> vehicles4 = new List<SearchVehicle>();

            vehicles = repo.SearchNew(noParams);

            Assert.AreEqual(6, vehicles.Count());
            Assert.AreEqual(5, vehicles[0].VehicleID);
            Assert.AreEqual("Truck", vehicles[0].BodyStyleName);
            Assert.AreEqual("Black", vehicles[0].ColorName);
            Assert.AreEqual("Ford", vehicles[0].MakeName);
            Assert.AreEqual("Mustang", vehicles[0].ModelName);
            Assert.AreEqual(2009, vehicles[0].Year);
            Assert.AreEqual(40000, vehicles[0].MSRP);
            Assert.AreEqual(38500, vehicles[0].SalePrice);
            Assert.AreEqual("inventory-5", vehicles[0].PictureFileName);
            Assert.AreEqual("Automatic", vehicles[0].Transmission);
            Assert.AreEqual("Black", vehicles[0].Interior);
            Assert.AreEqual("New", vehicles[0].Mileage);
            Assert.AreEqual("1J4GZ58S7VC697710", vehicles[0].VinNumber);

            makeMinPriceMaxPrice.Make = "Audi";
            makeMinPriceMaxPrice.MinPrice = 30000;
            makeMinPriceMaxPrice.MaxPrice = 33000;

            vehicles2 = repo.SearchNew(makeMinPriceMaxPrice);

            Assert.AreEqual(2, vehicles2.Count());
            Assert.AreEqual(3, vehicles2[0].VehicleID);
            Assert.AreEqual("Car", vehicles2[0].BodyStyleName);
            Assert.AreEqual("White", vehicles2[0].ColorName);
            Assert.AreEqual("Audi", vehicles2[0].MakeName);
            Assert.AreEqual("A6", vehicles2[0].ModelName);
            Assert.AreEqual(2018, vehicles2[0].Year);
            Assert.AreEqual(35000, vehicles2[0].MSRP);
            Assert.AreEqual(32000, vehicles2[0].SalePrice);
            Assert.AreEqual("inventory-3", vehicles2[0].PictureFileName);
            Assert.AreEqual("Automatic", vehicles2[0].Transmission);
            Assert.AreEqual("Black", vehicles2[0].Interior);
            Assert.AreEqual("New", vehicles2[0].Mileage);
            Assert.AreEqual("JH4DB1660LS017594", vehicles2[0].VinNumber);

            modelMinYearMaxYear.Model = "F";
            modelMinYearMaxYear.MinYear = 2009;
            modelMinYearMaxYear.MaxYear = 2011;

            vehicles3 = repo.SearchNew(modelMinYearMaxYear);

            Assert.AreEqual(1, vehicles3.Count());
            Assert.AreEqual(7, vehicles3[0].VehicleID);
            Assert.AreEqual("Car", vehicles3[0].BodyStyleName);
            Assert.AreEqual("Blue", vehicles3[0].ColorName);
            Assert.AreEqual("Ford", vehicles3[0].MakeName);
            Assert.AreEqual("Fusion", vehicles3[0].ModelName);
            Assert.AreEqual(2009, vehicles3[0].Year);
            Assert.AreEqual(14875, vehicles3[0].MSRP);
            Assert.AreEqual(13750, vehicles3[0].SalePrice);
            Assert.AreEqual("inventory-7", vehicles3[0].PictureFileName);
            Assert.AreEqual("Automatic", vehicles3[0].Transmission);
            Assert.AreEqual("Black", vehicles3[0].Interior);
            Assert.AreEqual("New", vehicles3[0].Mileage);
            Assert.AreEqual("JH4KA4660JC005061", vehicles3[0].VinNumber);

            yearMinPriceMaxPrice.Year = 2012;
            yearMinPriceMaxPrice.MaxPrice = 37000;

            vehicles4 = repo.SearchNew(yearMinPriceMaxPrice);

            Assert.AreEqual(1, vehicles4.Count());
            Assert.AreEqual(8, vehicles4[0].VehicleID);
            Assert.AreEqual("Truck", vehicles4[0].BodyStyleName);
            Assert.AreEqual("Grey", vehicles4[0].ColorName);
            Assert.AreEqual("Ford", vehicles4[0].MakeName);
            Assert.AreEqual("F-150", vehicles4[0].ModelName);
            Assert.AreEqual(2012, vehicles4[0].Year);
            Assert.AreEqual(35000, vehicles4[0].MSRP);
            Assert.AreEqual(33000, vehicles4[0].SalePrice);
            Assert.AreEqual("inventory-8", vehicles4[0].PictureFileName);
            Assert.AreEqual("Automatic", vehicles4[0].Transmission);
            Assert.AreEqual("Grey", vehicles4[0].Interior);
            Assert.AreEqual("New", vehicles4[0].Mileage);
            Assert.AreEqual("WDCGG8HB0AF462890", vehicles4[0].VinNumber);
            
        }

        [Test]
        public void CanSearchUsed()
        {
            var repo = new VehiclesRepository();

            SearchVehicleParameters noParams = new SearchVehicleParameters();
            SearchVehicleParameters makeMinPriceMaxPrice = new SearchVehicleParameters();
            SearchVehicleParameters modelMinYearMaxYear = new SearchVehicleParameters();
            SearchVehicleParameters yearMinPriceMaxPrice = new SearchVehicleParameters();

            List<SearchVehicle> vehicles = new List<SearchVehicle>();
            List<SearchVehicle> vehicles2 = new List<SearchVehicle>();
            List<SearchVehicle> vehicles3 = new List<SearchVehicle>();
            List<SearchVehicle> vehicles4 = new List<SearchVehicle>();

            vehicles = repo.SearchUsed(noParams);

            Assert.AreEqual(6, vehicles.Count());
            Assert.AreEqual(6, vehicles[0].VehicleID);
            Assert.AreEqual("SUV", vehicles[0].BodyStyleName);
            Assert.AreEqual("Yellow", vehicles[0].ColorName);
            Assert.AreEqual("Ford", vehicles[0].MakeName);
            Assert.AreEqual("Explorer", vehicles[0].ModelName);
            Assert.AreEqual(2012, vehicles[0].Year);
            Assert.AreEqual(45000, vehicles[0].MSRP);
            Assert.AreEqual(42000, vehicles[0].SalePrice);
            Assert.AreEqual("inventory-6", vehicles[0].PictureFileName);
            Assert.AreEqual("Automatic", vehicles[0].Transmission);
            Assert.AreEqual("Grey", vehicles[0].Interior);
            Assert.AreEqual("77687", vehicles[0].Mileage);
            Assert.AreEqual("2C3CA5CG8BH558973", vehicles[0].VinNumber);

            makeMinPriceMaxPrice.Make = "Toyota";
            makeMinPriceMaxPrice.MaxPrice = 33000;

            vehicles2 = repo.SearchUsed(makeMinPriceMaxPrice);

            Assert.AreEqual(3, vehicles2.Count());
            Assert.AreEqual(12, vehicles2[0].VehicleID);
            Assert.AreEqual("Truck", vehicles2[0].BodyStyleName);
            Assert.AreEqual("Silver", vehicles2[0].ColorName);
            Assert.AreEqual("Toyota", vehicles2[0].MakeName);
            Assert.AreEqual("Tacoma", vehicles2[0].ModelName);
            Assert.AreEqual(2012, vehicles2[0].Year);
            Assert.AreEqual(20200, vehicles2[0].MSRP);
            Assert.AreEqual(20000, vehicles2[0].SalePrice);
            Assert.AreEqual("inventory-12", vehicles2[0].PictureFileName);
            Assert.AreEqual("Automatic", vehicles2[0].Transmission);
            Assert.AreEqual("Black", vehicles2[0].Interior);
            Assert.AreEqual("5000", vehicles2[0].Mileage);
            Assert.AreEqual("1J4FJ78L5KL525073", vehicles2[0].VinNumber);

            modelMinYearMaxYear.Model = "A";
            modelMinYearMaxYear.MinYear = 2009;

            vehicles3 = repo.SearchUsed(modelMinYearMaxYear);

            Assert.AreEqual(1, vehicles3.Count());
            Assert.AreEqual(2, vehicles3[0].VehicleID);
            Assert.AreEqual("Car", vehicles3[0].BodyStyleName);
            Assert.AreEqual("Grey", vehicles3[0].ColorName);
            Assert.AreEqual("Audi", vehicles3[0].MakeName);
            Assert.AreEqual("A3", vehicles3[0].ModelName);
            Assert.AreEqual(2020, vehicles3[0].Year);
            Assert.AreEqual(12200, vehicles3[0].MSRP);
            Assert.AreEqual(11000, vehicles3[0].SalePrice);
            Assert.AreEqual("inventory-2", vehicles3[0].PictureFileName);
            Assert.AreEqual("Automatic", vehicles3[0].Transmission);
            Assert.AreEqual("Grey", vehicles3[0].Interior);
            Assert.AreEqual("20000", vehicles3[0].Mileage);
            Assert.AreEqual("JH4DB7560SS004122", vehicles3[0].VinNumber);

            yearMinPriceMaxPrice.Year = 2020;
            yearMinPriceMaxPrice.MaxPrice = 10000;

            vehicles4 = repo.SearchUsed(yearMinPriceMaxPrice);

            Assert.AreEqual(0, vehicles4.Count());
        }

        [Test]
        public void CanGetNewVehiclesInventory()
        {
            var repo = new VehiclesRepository();

            List<InventoryReport> vehicles = new List<InventoryReport>();

            vehicles = repo.GetInventoryNew();

            Assert.AreEqual(4, vehicles.Count());
            Assert.AreEqual(2018, vehicles[0].Year);
            Assert.AreEqual("Audi", vehicles[0].Make);
            Assert.AreEqual("A6", vehicles[0].Model);
            Assert.AreEqual(1, vehicles[0].Count);
            Assert.AreEqual(35000, vehicles[0].StockValue);
        }

        [Test]
        public void CanGetUsedVehiclesInventory()
        {
            var repo = new VehiclesRepository();

            List<InventoryReport> vehicles = new List<InventoryReport>();

            vehicles = repo.GetInventoryUsed();

            Assert.AreEqual(3, vehicles.Count());
            Assert.AreEqual(2020, vehicles[0].Year);
            Assert.AreEqual("Audi", vehicles[0].Make);
            Assert.AreEqual("Q7", vehicles[0].Model);
            Assert.AreEqual(1, vehicles[0].Count);
            Assert.AreEqual(12200, vehicles[0].StockValue);
        }

        // Specials Tests
        [Test]
        public void CanGetAllSpecials()
        {
            var repo = new SpecialsRepository();

            List<Special> specials = new List<Special>();

            specials = repo.GetAll();

            Assert.AreEqual(5, specials.Count());
            Assert.AreEqual(1, specials[0].SpecialID);
            Assert.AreEqual("20% Off Used Cars", specials[0].Name);
            Assert.AreEqual("Now until September 2021", specials[0].Description);
        }

        [Test]
        public void CanAddSpecial()
        {
            var repo = new SpecialsRepository();

            Special special = new Special()
            {
                Name = "Test Name",
                Description = "Test Description"
            };


            repo.AddSpecial(special);
            var specials = repo.GetAll();

            Assert.AreEqual(7, specials.Count());
            Assert.AreEqual(7, specials[6].SpecialID);
            Assert.AreEqual("Test Name", specials[6].Name);
            Assert.AreEqual("Test Description", specials[6].Description);
        }
        
        [Test]
        public void CanDeleteSpecial()
        {
            var repo = new SpecialsRepository();

            List<Special> specials = new List<Special>();
            repo.DeleteSpecial(5);
            
            specials = repo.GetAll();

            Assert.AreEqual(4, specials.Count());
            Assert.AreEqual(4, specials[3].SpecialID);
            Assert.AreEqual("Free tire rotation for all used car purchases", specials[3].Name);
            Assert.AreEqual("Now until 2022", specials[3].Description);
        }


        // Sales Tests
        [Test]
        public void CanSearchAllSales()
        {
            var repo = new VehiclesRepository();
            var noParams = new SearchVehicleParameters();

            List<SearchVehicle> vehicles = repo.SearchSales(noParams);

            Assert.AreEqual(9, vehicles.Count());
            Assert.AreEqual(6, vehicles[0].VehicleID);
            Assert.AreEqual("SUV", vehicles[0].BodyStyleName);
            Assert.AreEqual("Yellow", vehicles[0].ColorName);
            Assert.AreEqual("Ford", vehicles[0].MakeName);
            Assert.AreEqual("Explorer", vehicles[0].ModelName);
            Assert.AreEqual(2012, vehicles[0].Year);
            Assert.AreEqual(45000, vehicles[0].MSRP);
            Assert.AreEqual(42000, vehicles[0].SalePrice);
            Assert.AreEqual("inventory-6", vehicles[0].PictureFileName);
            Assert.AreEqual("Automatic", vehicles[0].Transmission);
            Assert.AreEqual("Grey", vehicles[0].Interior);
            Assert.AreEqual("77687", vehicles[0].Mileage);
            Assert.AreEqual("2C3CA5CG8BH558973", vehicles[0].VinNumber);
        }

        [Test]
        public void CanSearchSalesReport()
        {
            var repo = new SalesRepository();

            // no parameter search
            var noParams = new SaleReportParameters();
            List<SaleReport> sales = repo.SearchSalesReport(noParams);

            Assert.AreEqual(2, sales.Count());
            Assert.AreEqual("Mark", sales[0].FirstName);
            Assert.AreEqual("McDermott", sales[0].LastName);
            Assert.AreEqual(44000, sales[0].TotalSales);
            Assert.AreEqual(2, sales[0].TotalVehicles);

            // user parameter search
            var userParam = new SaleReportParameters();
            userParam.User = "Jason Hills";
            List<SaleReport> sales2 = repo.SearchSalesReport(userParam);

            Assert.AreEqual(1, sales2.Count());
            Assert.AreEqual("Jason", sales2[0].FirstName);
            Assert.AreEqual("Hills", sales2[0].LastName);
            Assert.AreEqual(13750, sales2[0].TotalSales);
            Assert.AreEqual(1, sales2[0].TotalVehicles);

            // date parameter search
            var dateParam = new SaleReportParameters();
            userParam.FromDate = "1/20/21";
            userParam.ToDate = "9/20/21";
            List <SaleReport> sales3 = repo.SearchSalesReport(dateParam);

            Assert.AreEqual(2, sales3.Count());
            Assert.AreEqual("Mark", sales3[0].FirstName);
            Assert.AreEqual("McDermott", sales3[0].LastName);
            Assert.AreEqual(44000, sales3[0].TotalSales);
            Assert.AreEqual(2, sales3[0].TotalVehicles);


        }

        [Test]
        public void CanInsertSale()
        {
            var repo = new SalesRepository();
            
            Sale newSale = new Sale()
            {
                Name = "Timothy",
                Email = "timothy@gmail.com",
                PhoneNumber = "808-777-7777",
                Address1 = "Test address 1",
                Address2 = "",
                City = "Kailua",
                State = 1,
                Zipcode = "96734",
                PurchasePrice = 12000,
                VehicleID = 10,
                PurchaseType = "Cash",
                UserID = "00000000-0000-0000-0000-000000000000"
            };
            
            repo.Insert(newSale);
            Sale retrieved = repo.GetByID(4);
            Assert.AreEqual(4, retrieved.SaleID);
            Assert.AreEqual("Timothy", retrieved.Name);
            Assert.AreEqual("timothy@gmail.com", retrieved.Email);
            Assert.AreEqual("808-777-7777", retrieved.PhoneNumber);
            Assert.AreEqual("Test address 1", retrieved.Address1);
            Assert.AreEqual("", retrieved.Address2);
            Assert.AreEqual("Kailua", retrieved.City);
            Assert.AreEqual("CA", retrieved.State);
            Assert.AreEqual("96734", retrieved.Zipcode);
            Assert.AreEqual(12000, retrieved.PurchasePrice);
            Assert.AreEqual(10, retrieved.VehicleID);
            Assert.AreEqual("Cash", retrieved.PurchaseType);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", retrieved.UserID);
        }

        [Test]
        public void CanGetSaleByID()
        {
            var repo = new SalesRepository();

            Sale sale = repo.GetByID(2);

            Assert.AreEqual(2, sale.SaleID);
            Assert.AreEqual("Jasmine", sale.Name);
            Assert.AreEqual("Jasmine@gmail.com", sale.Email);
            Assert.AreEqual("808-555-4343", sale.PhoneNumber);
            Assert.AreEqual("170 N Kainalu Dr.", sale.Address1);
            Assert.AreEqual("", sale.Address2);
            Assert.AreEqual("Kailua", sale.City);
            Assert.AreEqual("WA", sale.State);
            Assert.AreEqual("96734", sale.Zipcode);
            Assert.AreEqual(24000, sale.PurchasePrice);
            Assert.AreEqual(2, sale.VehicleID);
            Assert.AreEqual("Bank Finance", sale.PurchaseType);
        }


        // Contacts Tests
        [Test]
        public void CanAddContact()
        {
            var repo = new ContactsRepository();

            Contact newContact = new Contact()
            {
                Name = "NewContact",
                Email = "TestEmail@yahoo.com",
                PhoneNumber = "808-111-2222",
                Message = "This is a test message"
            };

            repo.Insert(newContact);
            var contacts = repo.GetAll();

            Assert.AreEqual(7, contacts.Count());
            Assert.AreEqual(7, contacts[6].ContactID);
            Assert.AreEqual("NewContact", contacts[6].Name);
            Assert.AreEqual("TestEmail@yahoo.com", contacts[6].Email);
            Assert.AreEqual("808-111-2222", contacts[6].PhoneNumber);
            Assert.AreEqual("This is a test message", contacts[6].Message);
        }

        // Colors Tests
        [Test]
        public void CanGetAllColors()
        {
            var repo = new ColorsRepository();
            List<Color> colors = new List<Color>();

            colors = repo.GetAll();

            Assert.AreEqual(7, colors.Count());
            Assert.AreEqual(1, colors[0].ColorID);
            Assert.AreEqual("White", colors[0].ColorName);
        }

        // Body Styles tests
        [Test]
        public void CanGetAllBodyStyles()
        {
            var repo = new BodyStylesRepository();

            List<BodyStyle> styles = new List<BodyStyle>();

            styles = repo.GetAll();

            Assert.AreEqual(4, styles.Count());
            Assert.AreEqual(1, styles[0].BodyStyleID);
            Assert.AreEqual("Car", styles[0].BodyStyleName);

        }

        // States tests
        [Test]
        public void CanGetAllStates()
        {
            var repo = new StatesRepository();

            List<State> states = new List<State>();

            states = repo.GetAll();

            Assert.AreEqual(3, states.Count());
            Assert.AreEqual(1, states[0].StateID);
            Assert.AreEqual("HI", states[0].StateName);
        }

        // Users tests
        [Test]
        public void CanGetAllUsers()
        {
            var repo = new UsersRepository();

            List<User> users = new List<User>();

            users = repo.GetAll();

            Assert.AreEqual(2, users.Count());
            Assert.AreEqual("McDermott", users[0].LastName);
            Assert.AreEqual("Mark", users[0].FirstName);
            Assert.AreEqual("markpm@guildcars.com", users[0].Email);
            Assert.AreEqual("Admin", users[0].Role);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", users[0].UserID);
        }

        //[Test]
        //public void CanInsertUser()
        //{
        //}
     


        //Model Tests
        [Test]
        public void CanGetAllModels()
        {
            var repo = new ModelsRepository();

            List<Model> models = new List<Model>();

            models = repo.GetAll();

            Assert.AreEqual(28, models.Count());
            Assert.AreEqual(1, models[0].ModelID);
            Assert.AreEqual("R8", models[0].ModelName);
            Assert.AreEqual("Audi", models[0].MakeName);
            Assert.AreEqual("markpm@guildcars.com", models[0].UserEmail);
        }

        [Test]
        public void CanInsertModel()
        {
            var repo = new ModelsRepository();

            Model model = new Model()
            {
                ModelName = "Prius",
                MakeName = "Toyota",
                UserEmail = "markpm@guildcars.com"
            };


            repo.Insert(model);
            var models = repo.GetAll();

            Assert.AreEqual(29, models.Count());
            Assert.AreEqual(29, models[28].ModelID);
            Assert.AreEqual("Prius", models[28].ModelName);
            Assert.AreEqual("Toyota", models[28].MakeName);
            Assert.AreEqual("markpm@guildcars.com", models[28].UserEmail);
        }

        //Make Tests
        [Test]
        public void CanGetAllMakes()
        {
            var repo = new MakesRepository();

            List<Make> makes = new List<Make>();

            makes = repo.GetAll();

            Assert.AreEqual(7, makes.Count());
            Assert.AreEqual(1, makes[0].MakeID);
            Assert.AreEqual("Audi", makes[0].MakeName);
            Assert.AreEqual("markpm@guildcars.com", makes[0].UserEmail);
        }

        [Test]
        public void CanInsertMake()
        {
            var repo = new MakesRepository();

            Make make = new Make()
            {
                MakeName = "Acura",
                UserEmail = "markpm@guildcars.com"
            };


            repo.Insert(make);
            var makes = repo.GetAll();

            Assert.AreEqual(8, makes.Count());
            Assert.AreEqual(8, makes[7].MakeID);
            Assert.AreEqual("Acura", makes[7].MakeName);
            Assert.AreEqual("markpm@guildcars.com", makes[7].UserEmail);
        }


        [Test]
        public void CanGetFeatured()
        {
            var repo = new VehiclesRepository();

            var featuredID = repo.GetFeatured(true);
            Assert.AreEqual(1, featuredID);
        }

        [Test]
        public void CanGetColor()
        {
            var repo = new VehiclesRepository();

            var colorID = repo.GetColor("Silver");
            Assert.AreEqual(7, colorID);
        }


    }
}
