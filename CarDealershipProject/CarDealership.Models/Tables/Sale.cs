using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class Sale
    {
        public int SaleID { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter an address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Please enter a city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please select a state")]
        public int State { get; set; }
        [Required]
        [RegularExpression(@"^(\d{5})$", ErrorMessage = "Please enter a valid 5 digit zipcode")]
        public string Zipcode { get; set; }
        [Required]
        public decimal? PurchasePrice { get; set; }
        [Required(ErrorMessage = "Please select a purchase type")]
        public string PurchaseType { get; set; }
        public DateTime SaleDate { get; set; }
        public int VehicleID { get; set; }
        public string UserID { get; set; }
    }
}
