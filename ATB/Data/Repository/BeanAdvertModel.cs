using System;
using ATB.BackOffice.Api;

namespace ATB.Data.Repository
{
	public class BeanAdvertModel
	{
		public Guid Id { get; }
		public Guid ImageId { get; }
		public string Image64 { get; }
		public double Cost { get; }
		public string Name { get; }
		public string Aroma { get; }
		public string Colour { get; }
		public DateTime Date { get; }

		public BeanAdvertModel(UploadModel model)
		{
			Id = Guid.NewGuid();
			ImageId = Guid.NewGuid();
			Image64 = model.Image64;
			Cost = model.Cost;
			Name = model.Name;
			Aroma = model.Aroma;
			Colour = model.Colour;
			Date = model.Date;
		}
	}
}