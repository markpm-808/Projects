using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class PurchaseVehicleViewModel
    {
        public DetailsVehicle Details { get; set; }
        public List<State> States { get; set; }
        public List<PurchaseType> Types { get; set; }
        public Sale Sale { get; set; }

    }
}