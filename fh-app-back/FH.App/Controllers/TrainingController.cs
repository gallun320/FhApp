using fh_app_back.Features.Training;
using FH.App.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace fh_app_back.Controllers
{
	public class TrainingController : BaseControllerApi
	{
		private readonly TrainingCreateHandler _trainingCreateHandler;
		
		public TrainingController(
			ILogger<TrainingController> logger, 
			TrainingCreateHandler trainingCreateHandler) 
			: base(logger)
		{
			_trainingCreateHandler = trainingCreateHandler;
		}

		[HttpPost]
		public async Task<IActionResult> CreateTraining([FromBody] TrainingCreateCommand createCommand, CancellationToken cancellationToken)
		{
			return MakeResponse(await _trainingCreateHandler.HandlerAsync(createCommand, cancellationToken));
		}

		
	}
}