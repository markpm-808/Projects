using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class Special
    {
        public int SpecialID { get; set; }
        [Required(ErrorMessage ="Please enter a title")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please enter a description")]
        public string Description { get; set; }
    }
}
