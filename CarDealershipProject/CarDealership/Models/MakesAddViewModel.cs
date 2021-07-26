using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class MakesAddViewModel
    {
        public List<Make> Makes { get; set; }
        public Make NewMake { get; set; }
    }
}