using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAssessment.Models
{
    public class UpdateDvdRequest
    {   
        public int id { get; set; }
        public string title { get; set; }
        public int releaseYear { get; set; }
        public string director { get; set; }
        public string rating { get; set; }
        public string notes { get; set; }
    }
}
