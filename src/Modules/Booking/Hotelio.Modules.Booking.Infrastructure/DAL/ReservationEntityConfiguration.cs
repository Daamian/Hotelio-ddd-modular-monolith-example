using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Hotelio.Modules.Booking.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelio.Modules.Booking.Infrastructure.DAL;

internal class ReservationEntityConfiguration: IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("_id");
        builder.Ignore(r => r.Events);
        builder.Ignore(r => r.Amenities);
        //builder.HasKey("_id");
        //builder.Ignore(r => r.Events);
        builder
            .Property<string>("HotelId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("hotel_id")
            .IsRequired(true);
        builder
            .Property<string>("OwnerId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("owner_id")
            .IsRequired(true);
        builder
            .Property<int>("RoomType")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("room_type")
            .IsRequired(true);
        builder
            .Property<string?>("RoomId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("room_id")
            .IsRequired(false);
        builder
            .Property<int>("NumberOfGuests")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("number_of_guests")
            .IsRequired(true);
        builder
            .Property<Status>("Status")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("status")
            .IsRequired(true);
        builder
            .Property<double>("PriceToPay")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("price_to_pay")
            .IsRequired(true);
        builder
            .Property<double>("PricePayed")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("price_payed")
            .IsRequired(true);
        builder
            .Property<PaymentType>("PaymentType")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("payment_type")
            .IsRequired(true);
        
        builder.OwnsOne<DateRange>("DateRange", y =>
        {
            y.Property(p => p.StartDate).HasColumnName("start_date");
            y.Property(p => p.EndDate).HasColumnName("end_date");
        });

        builder.Property<List<Amenity>>("_amenities")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<List<Amenity>>(v, new JsonSerializerOptions()),
                new ValueComparer<List<Amenity>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList())
            )
            .HasColumnName("amenities");
    }
}
