using FH.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FH.Infrastructure.Context;

/// <summary>
/// Extension for setup scheme
/// </summary>
public static class DbContextModelBuilderExtensions
{
    /// <summary>
    /// Disable cascade deleting
    /// </summary>
    public static void DisableCascadeDeletes(this ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }

    /// <summary>
    /// Convert enums to string representation 
    /// </summary>
    public static void SetEnumToStringConversion(this ModelBuilder modelBuilder, int maxLength = 128)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                var type = property.ClrType.GetNullableUnderlyingType();
                if (type.IsEnum)
                {
                    var converterType = typeof(EnumToStringConverter<>).MakeGenericType(type);
                    var converter = (ValueConverter?)Activator.CreateInstance(converterType);
                    property.SetValueConverter(converter);
                    property.SetMaxLength(maxLength);
                }
            }
        }
    }
}