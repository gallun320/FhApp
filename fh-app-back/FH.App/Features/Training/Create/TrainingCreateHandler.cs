using FH.Domain.Entities;
using FH.Domain.Exceptions;
using FH.Domain.Repositories;
using FH.Domain.RepositoryInterfaces;

namespace fh_app_back.Features.Training
{
	public class TrainingCreateHandler
	{
		private readonly ITrainingRepository _trainingRepository;

		public TrainingCreateHandler(ITrainingRepository trainingRepository)
		{
			_trainingRepository = trainingRepository;
		}

		public async Task<TrainingCreateResult> HandlerAsync(TrainingCreateCommand command, CancellationToken cancellationToken)
		{
			var training = await _trainingRepository.GetOneAsync(
				entity => entity.Name == command.Name, cancellationToken);

			if (training != null)
				throw new FhException($"Training already exist");

			var result = await _trainingRepository.AddAsync(new TrainingEntity {
					Name = command.Name
				},
				cancellationToken);

			return new(result.Id);
		}
	}
}