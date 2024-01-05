using Hotelio.Modules.Booking.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.Booking.Infrastructure.DAL;

internal class ReservationDbContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("reservations");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    
}