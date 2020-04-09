using System.Threading.Tasks;
using ATB.BackOffice.BeanAdvert.Data;
using ATB.BackOffice.BeanAdvert.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ATB.BackOffice.BeanAdvert.Api
{
    [ApiController]
    [Route("[controller]")]
    public class BeanAdvertAdminController : ControllerBase
    {
        private readonly ILogger<BeanAdvertAdminController> logger;
        private readonly IBeanAdvertCommand beanAdvertCommand;
        private readonly IBeanAdvertManager beanAdvertManager;

        public BeanAdvertAdminController(IBeanAdvertCommand beanAdvertCommand, ILogger<BeanAdvertAdminController> logger, IBeanAdvertManager beanAdvertManager)
        {
            this.beanAdvertCommand = beanAdvertCommand;
            this.logger = logger;
            this.beanAdvertManager = beanAdvertManager;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(UploadModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dateStatus = await this.beanAdvertManager.GetDateStatus(model.Date);

            if (dateStatus != DateStatus.Free)
                return new BadRequestObjectResult(new { dateStatus = dateStatus.ToString() });

            var results = await this.beanAdvertCommand.StoreBean(new BeanAdvertModel(model));

            if (results > 0)
                return new OkObjectResult(new { dateStatus = dateStatus.ToString() });
            
            return new BadRequestObjectResult(new { dateStatus = DateStatus.Error });
        }
    }
}