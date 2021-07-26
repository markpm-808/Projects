using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Queries
{
    public class SearchVehicle
    {
        public int VehicleID { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string BodyStyleName { get; set; }
        public string ColorName { get; set; }
        public int Year { get; set; }
        public string MSRP { get; set; }
        public string SalePrice { get; set; }
        public string PictureFileName { get; set; }
        public string Transmission { get; set; }
        public string Interior { get; set; }
        public string Mileage { get; set; }
        public string VinNumber { get; set; }
    }
}
