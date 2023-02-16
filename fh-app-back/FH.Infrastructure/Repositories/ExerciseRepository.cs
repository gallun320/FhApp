using FH.Domain.Entities;
using FH.Domain.RepositoryInterfaces;
using FH.Infrastructure.Context;
using Microsoft.Extensions.Logging;

namespace FH.Infrastructure.Repositories
{
	public class ExerciseRepository : BaseRepository<ExerciseEntity>, IExerciseRepository
	{

		public ExerciseRepository(ApplicationDbContext dbContext, ILogger<ExerciseRepository> logger) 
			: base(dbContext, logger)
		{
		}
	}
}