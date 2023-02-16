using FH.Domain.Entities;
using FH.Domain.Exceptions;
using FH.Domain.RepositoryInterfaces;

namespace FH.App.Features.Training.Create
{
	public class CreateTrainingHandler
	{
		private readonly ITrainingRepository _trainingRepository;

		public CreateTrainingHandler(ITrainingRepository trainingRepository)
		{
			_trainingRepository = trainingRepository;
		}

		public async Task<CreateTrainingDto> HandlerAsync(CreateTrainingCommand command, CancellationToken cancellationToken)
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