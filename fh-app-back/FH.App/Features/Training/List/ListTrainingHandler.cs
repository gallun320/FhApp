using FH.Domain.RepositoryInterfaces;

namespace FH.App.Features.Training.List;

public class ListTrainingHandler
{
    private readonly ITrainingRepository _repository;

    public ListTrainingHandler(ITrainingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<ListTrainingItemDto>> HandleAsync(CancellationToken cancellationToken)
    {
        var trainingsEntities = await _repository.GetAsync(default, cancellationToken);

        var result = trainingsEntities.Select(training => 
            new ListTrainingItemDto(training.Id, training.Name))
            .ToList();

        return result;
    }
} 