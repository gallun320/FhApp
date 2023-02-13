using FH.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Qorp.Domain.Context.Abstract;

namespace FH.Domain.Context
{
	public class ApplicationContext : BaseDbContext
	{

		public ApplicationContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.DisableCascadeDeletes();
			modelBuilder.SetEnumToStringConversion(32);

			modelBuilder.Entity<TrainingEntity>()
				.HasMany(c => c.Exercises)
				.WithOne(e => e.Training);

			modelBuilder.Entity<TrainingEntity>().Navigation(entity => entity.Exercises).AutoInclude();

			modelBuilder.Entity<ExerciseEntity>().Navigation(entity => entity.Training).AutoInclude();

			base.OnModelCreating(modelBuilder);
		}
		
		public DbSet<TrainingEntity> Trainings { get; set; }
		public DbSet<ExerciseEntity> Exercises { get; set; }
	}
}