using System.Threading.Tasks;
using ATB.Infrastructure;
using ATB.Infrastructure.DataLayer;

namespace ATB.Data.Repository
{
	public class BeanAdvertRepository : IBeanAdvertRepository
	{
		private readonly IGateway gateway;

		public BeanAdvertRepository(IGateway gateway)
		{
			this.gateway = gateway;
		}

		public async Task<ExecutionResult<int>> Insert(BeanAdvertModel model)
		{
			return await this.gateway.ExecuteCommand(@"
            INSERT
            INTO
                beans
                (
                      id
                    , cost
                    , bean_name
                    , aroma
                    , colour
                    , image_id
                    , date
                )
            VALUES 
                (  
                      @Id
                    , @Cost 
                    , @BeanName 
                    , @Aroma 
                    , @Colour
                    , @ImageId
                    , @Date
                )
            ", new
			{
				Id = model.Id,
				Cost = model.Cost,
				BeanName = model.Name,
				Aroma = model.Aroma,
				Colour = model.Colour,
				ImageId = model.ImageId,
				Date = model.Date.Date
			});
		}
	}
}