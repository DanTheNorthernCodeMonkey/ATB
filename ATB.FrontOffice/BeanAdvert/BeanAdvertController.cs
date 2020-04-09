using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ATB.FrontOffice.BeanAdvert
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

            if (bean == null)
                return Ok();

            return new OkObjectResult(bean);
        }
    }
}