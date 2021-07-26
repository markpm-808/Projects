using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class HomePageViewModel
    {
        public List<CarDealership.Models.Queries.FeaturedVehicle> FeaturedModel { get; set; }
        public List<CarDealership.Models.Tables.Special> SpecialsModel { get; set; }
    }
}