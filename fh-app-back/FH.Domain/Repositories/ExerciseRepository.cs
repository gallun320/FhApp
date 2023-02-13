using FH.Domain.Context;
using FH.Domain.Entities;
using FH.Domain.Repositories.Abstract;
using Microsoft.Extensions.Logging;

namespace FH.Domain.Repositories
{
	public class ExerciseRepository : BaseRepository<ExerciseEntity>
	{

		public ExerciseRepository(ApplicationContext context, ILogger<ExerciseRepository> logger) 
			: base(context, logger)
		{
		}
	}
}