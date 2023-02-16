using FH.Domain.Exceptions;
using FH.Domain.RepositoryInterfaces;
using FH.Infrastructure.Extensions;

namespace FH.App.Features.Training.Delete;

public class DeleteTrainingHandler
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ITrainingRepository _trainingRepository;

    public DeleteTrainingHandler(
        IExerciseRepository exerciseRepository, 
        ITrainingRepository trainingRepository)
    {
        _exerciseRepository = exerciseRepository;
        _trainingRepository = trainingRepository;
    }

    public async Task<bool> HandleAsync(DeleteTrainingCommand command, CancellationToken cancellationToken)
    {
        if (!command.Id.HasValue)
        {
            var exercises = await _exerciseRepository.GetAsync(default, cancellationToken);
            await exercises.ForEachAsync(async item =>
                await _exerciseRepository.DeleteAsync(item, cancellationToken));

            var trainings = await _trainingRepository.GetAsync(default, cancellationToken);
            await trainings.ForEachAsync(async item =>
                await _trainingRepository.DeleteAsync(item, cancellationToken));
            return true;
        }

        var training = await _trainingRepository
            .GetOneAsync(entity => entity.Id == command.Id, cancellationToken);

        if (training == null)
        {
            throw new FhException($"Training is not exist");
        }
        
        var exercisesConcrete = await _exerciseRepository
            .GetAsync(entity => entity.TrainingId == command.Id, cancellationToken);
        await exercisesConcrete.ForEachAsync(async item =>
            await _exerciseRepository.DeleteAsync(item, cancellationToken));

        return await _trainingRepository.DeleteAsync(training, cancellationToken);
    }
}