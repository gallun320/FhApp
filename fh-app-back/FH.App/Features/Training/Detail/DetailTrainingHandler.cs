using FH.Domain.RepositoryInterfaces;

namespace FH.App.Features.Training.Detail;

public class DetailTrainingHandler
{
    private readonly IExerciseRepository _exerciseRepository;

    public DetailTrainingHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<IList<ExerciseItemDto>> HandleAsync(
        DetailTrainingQuery query, CancellationToken cancellationToken)
    {
        var exercisesEntity =
            await _exerciseRepository.GetAsync(
                entity => entity.TrainingId == query.TrainingId, cancellationToken);

        return exercisesEntity
            .Select(exer => new ExerciseItemDto(exer.Id, exer.Name, exer.Iterations, exer.Repeats))
            .ToList();
    }
}