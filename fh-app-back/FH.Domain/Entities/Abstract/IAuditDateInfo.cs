namespace FH.Domain.Entities.Abstract;

/// <summary>
/// Interface date audit date
/// </summary>
public interface IAuditDateInfo
{
    /// <summary>
    /// Date creation entity
    /// </summary>
    DateTime Created { get; set; }

    /// <summary>
    /// Date update entity
    /// </summary>
    DateTime Updated { get; set; }
}