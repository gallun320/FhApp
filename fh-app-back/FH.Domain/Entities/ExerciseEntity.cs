using System.ComponentModel.DataAnnotations.Schema;
using FH.Domain.Entities.Abstract;

namespace FH.Domain.Entities
{
	public class ExerciseEntity : IBaseEntity, IArchivable
	{
		public long Id { get; set; }
		
		public long TrainingId { get; set; }

		[ForeignKey(nameof(TrainingId))]
		public TrainingEntity Training { get; set; } = null!;

		public string Name { get; set; } = null!;
		
		public long Iterations { get; set; }
		
		public long Repeats { get; set; }
		
		public bool IsActive { get; set; }
	}
}