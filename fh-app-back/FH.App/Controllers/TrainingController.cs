using FH.App.Controllers.Abstract;
using FH.App.Features.Training.Create;
using Microsoft.AspNetCore.Mvc;

namespace fh_app_back.Controllers
{
	public class TrainingController : BaseControllerApi
	{
		private readonly CreateTrainingHandler _createTrainingHandler;
		
		public TrainingController(
			ILogger<TrainingController> logger, 
			CreateTrainingHandler createTrainingHandler) 
			: base(logger)
		{
			_createTrainingHandler = createTrainingHandler;
		}

		[HttpPost]
		public async Task<IActionResult> CreateTraining([FromBody] CreateTrainingCommand command, CancellationToken cancellationToken)
		{
			return MakeResponse(await _createTrainingHandler.HandlerAsync(command, cancellationToken));
		}

		
	}
}