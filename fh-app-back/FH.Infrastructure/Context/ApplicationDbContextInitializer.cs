using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FH.Infrastructure.Context;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _dbContext;

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger, 
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _dbContext.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Something error while initialize db: {e.Message}", e);
            throw;
        }
    }
}