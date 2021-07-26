using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IVehiclesRepository
    {
        DetailsVehicle GetDetails(int vehicleId);
        Vehicle GetByID(int vehicleId);
        vehicleEdit EditGetByID(int vehicleId);
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(int vehicleId);
        List<SearchVehicle> SearchNew(SearchVehicleParameters parameters);
        List<SearchVehicle> SearchUsed(SearchVehicleParameters parameters);
        List<SearchVehicle> SearchSales(SearchVehicleParameters parameters);
        List<FeaturedVehicle> GetAllFeaturedVehicles();
        int GetFeatured(bool featured);
        int GetMake(string make);
        int GetModel(string model);
        int GetBodyStyle(string bodyStyle);
        int GetColor(string color);
        int GetType(string type);
        List<InventoryReport> GetInventoryNew();
        List<InventoryReport> GetInventoryUsed();
        void UpdateFileName(string vinNumber, string pictureFileName);
    }
}
