using System;
using System.ComponentModel.DataAnnotations;

namespace ATB.BackOffice.Api
{
    public class UploadModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public string Aroma { get; set;}

        [Required]
        public string Colour { get; set;}

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Image64 { get; set; }
    }
}