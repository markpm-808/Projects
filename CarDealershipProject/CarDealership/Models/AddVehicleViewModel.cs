using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class AddVehicleViewModel : IValidatableObject
    {
        public List<Make> Makes { get; set; }
        public List<Model> Models { get; set; }
        public List<VehicleType> VehicleTypes { get; set; }
        public List<BodyStyle> BodyStyles { get; set; }
        public List<Color> Colors { get; set; }
        public List<Color> InteriorColors { get; set; }
        public Vehicle NewVehicle { get; set; }
        [Required(ErrorMessage ="Please upload a Picture")]
        public HttpPostedFileBase ImageUpload { get; set; }
        public bool Checkbox { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".png", ".jpg", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be a png, jpg, or jpeg."));
                }
            }

            return errors;
        }
    }
}