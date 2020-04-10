using System.Threading.Tasks;
using ATB.FrontOffice.Domain;
using ATB.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ATB.FrontOffice.Api
{
    [ApiController]
    [Route("[controller]")]
    public class BeanAdvertController : ControllerBase
    {
        private readonly ILogger<BeanAdvertController> _logger;
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
                return NoContent();

            return new OkObjectResult(bean.Data);
        }
    }
}