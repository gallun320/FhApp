using FH.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FH.Infrastructure.Context.Configurations;

public class TrainingEntityConfiguration : IEntityTypeConfiguration<TrainingEntity>
{
    public void Configure(EntityTypeBuilder<TrainingEntity> builder)
    {
	    builder.HasMany(c => c.Exercises)
		    .WithOne(e => e.Training);
        
	    builder.Navigation(entity => entity.Exercises).AutoInclude();
    }
}