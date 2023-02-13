using FH.Domain.Context;
using FH.Domain.Entities;
using FH.Domain.Repositories.Abstract;
using Microsoft.Extensions.Logging;

namespace FH.Domain.Repositories
{
	public class TrainingRepository : BaseRepository<TrainingEntity>
	{

		public TrainingRepository(ApplicationContext context, ILogger<TrainingRepository> logger) : base(context, logger)
		{
		}
	}
}