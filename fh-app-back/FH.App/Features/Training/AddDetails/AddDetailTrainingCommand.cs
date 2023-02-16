namespace FH.App.Features.Training.AddDetails;

public record ExerciseItemCommand(
    string Name,
    long Iterations,
    long Repeats);

public record AddDetailTrainingCommand(long Id, IList<ExerciseItemCommand> List);