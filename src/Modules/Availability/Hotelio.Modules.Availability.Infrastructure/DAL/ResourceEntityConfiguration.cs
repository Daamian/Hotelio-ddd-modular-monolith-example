using Hotelio.Modules.Availability.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelio.Modules.Availability.Infrastructure.DAL;

internal class ResourceEntityConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey("_id");
        builder.Ignore(r => r.Events);
        builder
            .Property<string>("_groupId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("group_id")
            .IsRequired(true);
        builder
            .Property<int>("_type")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("type")
            .IsRequired(true);
        builder.Property<bool>("_isActive")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_active")
            .IsRequired(true);
        builder.HasMany<Book>("_books")
            .WithOne()
            .HasForeignKey("resource_id").IsRequired(true);
    }
}