using FH.App.Controllers.Abstract;
using FH.App.Features.Training.AddDetails;
using FH.App.Features.Training.Create;
using FH.App.Features.Training.Delete;
using FH.App.Features.Training.Detail;
using FH.App.Features.Training.List;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers
{
	public class TrainingController : BaseControllerApi
	{
		private readonly CreateTrainingHandler _createTrainingHandler;
		private readonly AddDetailTrainingHandler _addDetailTrainingHandler;
		private readonly DetailTrainingHandler _detailTrainingHandler;
		private readonly ListTrainingHandler _listTrainingHandler;
		private readonly DeleteTrainingHandler _deleteTrainingHandler;

		public TrainingController(
			ILogger<TrainingController> logger, 
			CreateTrainingHandler createTrainingHandler,
			AddDetailTrainingHandler addDetailTrainingHandler,
			DetailTrainingHandler detailTrainingHandler,
			ListTrainingHandler listTrainingHandler,
			DeleteTrainingHandler deleteTrainingHandler) 
			: base(logger)
		{
			_createTrainingHandler = createTrainingHandler;
			_addDetailTrainingHandler = addDetailTrainingHandler;
			_detailTrainingHandler = detailTrainingHandler;
			_listTrainingHandler = listTrainingHandler;
			_deleteTrainingHandler = deleteTrainingHandler;
		}

		[HttpPost]
		public async Task<IActionResult> CreateTrainingAsync([FromBody] CreateTrainingCommand command, CancellationToken cancellationToken)
		{
			return MakeResponse(await _createTrainingHandler.HandleAsync(command, cancellationToken));
		}

		[HttpPut]
		public async Task<IActionResult> AddDetailsTrainingAsync([FromBody] AddDetailTrainingCommand command, CancellationToken cancellationToken)
		{
			return MakeResponse(await _addDetailTrainingHandler.HandleAsync(command, cancellationToken));
		}
		
		[HttpGet("{id}")]
		public async Task<IActionResult> DetailsTrainingAsync([FromQuery] long id, CancellationToken cancellationToken)
		{
			return MakeResponse(await _detailTrainingHandler.HandleAsync(new(id), cancellationToken));
		}
		
		[HttpGet]
		public async Task<IActionResult> ListTrainingAsync(CancellationToken cancellationToken)
		{
			return MakeResponse(await _listTrainingHandler.HandleAsync(cancellationToken));
		}
		
		[HttpDelete]
		public async Task<IActionResult> DeleteTrainingAsync(
			[FromQuery] DeleteTrainingCommand command, CancellationToken cancellationToken)
		{
			return MakeResponse(await _deleteTrainingHandler.HandleAsync(command, cancellationToken));
		}
	}
}