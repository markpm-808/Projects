using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class InventoryReportViewModel
    {
        public List<InventoryReport> newVehicles { get; set; }
        public List<InventoryReport> usedVehicles { get; set; }
    }
}