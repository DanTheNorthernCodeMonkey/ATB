using System;
using System.Linq;
using System.Threading.Tasks;
using ATB.BackOffice.Domain;
using ATB.Data.ReadModel;
using ATB.Data.Repository;
using ATB.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ATB.BackOffice.Api
{
	[ApiController]
	[Route("[controller]")]
	public class BeanAdvertAdminController : ControllerBase
	{
		private readonly ILogger<BeanAdvertAdminController> logger;
		private readonly IBeanAdvertCommand beanAdvertCommand;
		private readonly IBeanAdvertManager beanAdvertManager;
		private readonly IBeanAdvertReadModel beanAdvertReadModel;

		public BeanAdvertAdminController(ILogger<BeanAdvertAdminController> logger, IBeanAdvertCommand beanAdvertCommand, IBeanAdvertManager beanAdvertManager, IBeanAdvertReadModel beanAdvertReadModel)
		{
			this.logger = logger;
			this.beanAdvertCommand = beanAdvertCommand;
			this.beanAdvertManager = beanAdvertManager;
			this.beanAdvertReadModel = beanAdvertReadModel;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var currentBeans = await this.beanAdvertReadModel.GetCurrentBeans(DateTime.UtcNow);

			if (currentBeans.Status == ExecutionStatus.Fail)
				return StatusCode(500);

			if (currentBeans.Status == ExecutionStatus.NoData)
				return NoContent();

			var dates = currentBeans.DataSet.Select(x => x.Date).ToList();

			return new OkObjectResult(dates);
		}

		[HttpPost]
		public async Task<IActionResult> Post(UploadModel model)
		{
			if (!ModelState.IsValid)
			{
				logger.Log(LogLevel.Debug, "model invalid");
				return BadRequest();
			}

			var dateStatus = await this.beanAdvertManager.GetDateStatus(model.Date);

			if (dateStatus != DateStatus.Free)
				return new BadRequestObjectResult(new { dateStatus = dateStatus.ToString() });

			var results = await this.beanAdvertCommand.StoreBean(new BeanAdvertModel(model));

			if (results == ExecutionStatus.Success)
				return new OkObjectResult(new { dateStatus = dateStatus.ToString(), takenDate = model.Date.Date });

			return StatusCode(500);
		}
	}
}