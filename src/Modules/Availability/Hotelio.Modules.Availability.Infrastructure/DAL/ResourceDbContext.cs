using Hotelio.Modules.Availability.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.Availability.Infrastructure.DAL;

internal class ResourceDbContext : DbContext
{
    public DbSet<Resource> Resources { get; set; }

    public ResourceDbContext(DbContextOptions<ResourceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("availability");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.ApplyConfiguration(new ResourceEntityConfiguration());
    }
}