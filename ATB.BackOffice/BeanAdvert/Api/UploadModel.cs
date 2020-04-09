using System;

namespace ATB.BackOffice.BeanAdvert.Api
{
    public class UploadModel
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Aroma { get; set;}
        public string Colour { get; set;}
        
        private DateTime date;

        public DateTime Date
        {
            get => date;
            set => date = value.AddHours(6).Date;
        }

        public string Image64 { get; set; }
    }
}