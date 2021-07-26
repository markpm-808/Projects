using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class EditVehicleViewModel
    {
        public List<Make> Makes { get; set; }
        public List<Model> Models { get; set; }
        public List<VehicleType> VehicleTypes { get; set; }
        public List<BodyStyle> BodyStyles { get; set; }
        public List<Color> Colors { get; set; }
        public List<Color> InteriorColors { get; set; }
        public vehicleEdit NewVehicle { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        public bool Checkbox { get; set; }
        public int Type { get; set; }
    }
}