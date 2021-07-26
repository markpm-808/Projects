using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class SpecialsAddViewModel
    {
        public List<Models.Tables.Special> Specials { get; set; }
        public Special AddSpecial { get; set; }
    }
}