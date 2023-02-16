using FH.Domain.Entities;
using FH.Domain.RepositoryInterfaces;
using FH.Infrastructure.Context;
using FH.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace FH.Domain.Repositories
{
	public class TrainingRepository : BaseRepository<TrainingEntity>, ITrainingRepository
	{

		public TrainingRepository(ApplicationDbContext dbContext, ILogger<TrainingRepository> logger) : base(dbContext, logger)
		{
		}
	}
}