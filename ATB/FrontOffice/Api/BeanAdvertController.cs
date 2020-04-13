using System.Threading.Tasks;
using ATB.FrontOffice.Domain;
using ATB.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ATB.FrontOffice.Api
{
    [ApiController]
    [Route("[controller]")]
    public class BeanAdvertController : ControllerBase
    {
        private readonly IBeanOfTheDayQuery beanOfTheDayQuery;

        public BeanAdvertController(IBeanOfTheDayQuery beanReadModel)
        {
            this.beanOfTheDayQuery = beanReadModel;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bean = await this.beanOfTheDayQuery.GetBeanOfTheDay();

            if (bean.Status == ExecutionStatus.Fail)
	            return StatusCode(500);

            if (bean.Status == ExecutionStatus.NoData)
                return NoContent();

            return new OkObjectResult(bean.Data);
        }
    }
}