using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class Model
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public DateTime DateAdded { get; set; }
        public string MakeName { get; set; }
        public string UserEmail { get; set; }
    }
}
