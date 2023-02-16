using FH.Domain.Entities;
using FH.Domain.Exceptions;
using FH.Domain.RepositoryInterfaces;

namespace FH.App.Features.Training.AddDetails;

public class AddDetailTrainingHandler
{
    private readonly ITrainingRepository _trainingRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public AddDetailTrainingHandler(
        ITrainingRepository trainingRepository,
        IExerciseRepository exerciseRepository)
    {
        _trainingRepository = trainingRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<bool> HandleAsync(AddDetailTrainingCommand trainingCommand, CancellationToken cancellationToken)
    {
        if (!(await _trainingRepository.AnyAsync(
                entity => entity.Id == trainingCommand.Id, cancellationToken)))
        {
            throw new FhException($"Training doesn't exist");
        }
        
        await _exerciseRepository.AddRangeAsync(trainingCommand.List.Select(item => new ExerciseEntity
        {
            Name = item.Name,
            Repeats = item.Repeats,
            Iterations = item.Iterations,
            TrainingId = trainingCommand.Id
        }).ToList(), cancellationToken);

        return true;
    }
}