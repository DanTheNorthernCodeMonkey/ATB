using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATB.Common.DataLayer;

namespace ATB.Common.BeanAdvert
{
    public class BeanAdvertReadModel : IBeanAdvertReadModel
    {
        private readonly IGateway gateway;

        public BeanAdvertReadModel(IGateway gateway)
        {
            this.gateway = gateway;
        }

        public async Task<BeanAdvertProjection> GetBeanFor(DateTime date)
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

        public async Task<IEnumerable<BeanAdvertProjection>> GetCurrentBeans(DateTime date)
        {
            return await this.gateway.GetCollection<BeanAdvertProjection>(@"
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