using FH.Domain.Entities.Abstract;

namespace FH.Domain.Entities
{
	public class TrainingEntity : IBaseEntity, IArchivable
	{
		public long Id { get; set; }

		public string Name { get; set; } = null!;

		public virtual ICollection<ExerciseEntity> Exercises { get; set; } = null; 

		public bool IsActive { get; set; }
	}
}