using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class ModelsAddViewModel
    {
        public List<Model> Models { get; set; }
        public List<Make> Makes { get; set; }
        public Model NewModel { get; set; }
    }
}