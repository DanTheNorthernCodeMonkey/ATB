using System;
using System.Threading.Tasks;
using ATB.Infrastructure;
using ATB.Infrastructure.DataLayer;

namespace ATB.Data.ReadModel
{
	public class BeanAdvertReadModel : IBeanAdvertReadModel
	{
		private readonly IGateway gateway;

		public BeanAdvertReadModel(IGateway gateway)
		{
			this.gateway = gateway;
		}

		public async Task<ExecutionResult<BeanAdvertProjection>> GetBeanFor(DateTime date)
		{
			return await this.gateway.Get<BeanAdvertProjection>(@"
            SELECT
                  id  
                , cost
                , bean_name
                , aroma
                , colour
                , image_id
                , date
            FROM
                beans
            WHERE
                date = @date
            ", new { date.Date });
		}

		public async Task<ExecutionResult<BeanAdvertProjection>> GetCurrentBeans(DateTime date)
		{
			return await this.gateway.Get<BeanAdvertProjection>(@"
            SELECT
                  id
                , cost
                , bean_name
                , aroma
                , colour
                , image_id
                , date
            FROM
                beans
            WHERE
                date >= @date
            ", new { date.Date });
		}
	}
}