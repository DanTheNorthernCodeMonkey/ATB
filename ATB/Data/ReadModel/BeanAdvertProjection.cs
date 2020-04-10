using System;

namespace ATB.Data.ReadModel
{
    public class BeanAdvertProjection
    {
        public Guid Id { get; }
        public decimal Cost { get; }
        public string BeanName { get; }
        public string Aroma { get; }
        public string Colour { get; }
        public Guid ImageId { get; }
        public DateTime Date { get; }
        public string ImageUri { get; set; }

        public BeanAdvertProjection(){}
        
        public BeanAdvertProjection(Guid Id, decimal cost, string bean_name, string aroma, string colour, Guid image_id, DateTime date)
        {
            Cost = cost;
            BeanName = bean_name;
            Aroma = aroma;
            Colour = colour;
            ImageId = image_id;
            Date = date;
        }
    }
}