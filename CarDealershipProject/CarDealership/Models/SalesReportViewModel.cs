using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class SalesReportViewModel
    {
        public List<User> Users { get; set; }
        public SaleReportParameters Parameters { get; set; }
    }
}