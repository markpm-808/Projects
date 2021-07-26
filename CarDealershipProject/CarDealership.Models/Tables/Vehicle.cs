using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarDealership.Models.Tables
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        [Required(ErrorMessage ="Please enter a Vin Number")]
        public string VinNumber { get; set; }
        [Required(ErrorMessage ="Please enter a Year")]
        [RegularExpression(@"^20((0[0-9])|(1[0-9])|(2[0-2]))$", ErrorMessage = "Year must be from 2000-2022")]
        public int Year { get; set; }
        public string Interior { get; set; }
        [Required(ErrorMessage = "Please enter the car's mileage")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string Mileage { get; set; }
        public string Transmission { get; set; }
        [Required(ErrorMessage = "Please enter the car's MSRP")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public decimal? MSRP { get; set; }
        [Required(ErrorMessage = "Please enter the car's sale price")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public decimal? SalePrice { get; set; }
        [Required(ErrorMessage ="Please enter a description")]
        public string Description { get; set; }
        public string PictureFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Featured { get; set; }
        public string MakeName { get; set; }
        public string TypeName { get; set; }
        public string ModelName { get; set; }
        public string BodyStyleName { get; set; }
        public string ColorName { get; set; }
    }
}
