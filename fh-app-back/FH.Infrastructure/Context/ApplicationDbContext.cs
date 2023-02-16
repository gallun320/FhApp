using System.Reflection;
using FH.Domain.Entities;
using FH.Infrastructure.Context.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace FH.Infrastructure.Context
{
	public class ApplicationDbContext : DbContext
	{
		private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

		public ApplicationDbContext(
			DbContextOptions options, 
			AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
		{
			_auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.DisableCascadeDeletes();
			modelBuilder.SetEnumToStringConversion(32);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
		}
		
		public DbSet<TrainingEntity> Trainings { get; set; }
		public DbSet<ExerciseEntity> Exercises { get; set; }
	}
}