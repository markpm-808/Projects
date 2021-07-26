using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DvdLibraryAssessment.Models.Queries
{
    public class InsertDvdRequest
    {   
        public string title { get; set; }
        public int releaseYear { get; set; }
        public string director { get; set; }
        public string rating { get; set; }
        public string notes { get; set; }
    }
}
