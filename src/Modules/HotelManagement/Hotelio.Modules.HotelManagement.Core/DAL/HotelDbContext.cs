using Hotelio.Modules.HotelManagement.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Core.DAL;

internal class HotelDbContext: DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("hotel_management");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .IsRequired();
        
        modelBuilder.Entity<Room>()
            .HasOne(e => e.Type)
            .WithMany()
            .HasForeignKey(e => e.RoomTypeId)
            .IsRequired();
        
        modelBuilder.Entity<Hotel>()
            .HasMany(e => e.Amenities)
            .WithMany();
    }
}