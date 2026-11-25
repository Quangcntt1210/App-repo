using APP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> b)
    {
        b.ToTable("bookings", t => t.HasCheckConstraint(
            "ck_bookings_valid", "quantity = 1 AND unit_price >= 0"));
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.ReservationId).HasColumnName("reservation_id");
        b.Property(x => x.UserId).HasColumnName("user_id");
        b.Property(x => x.BookingCode).HasColumnName("booking_code").HasMaxLength(30).IsRequired();
        b.Property(x => x.Quantity).HasColumnName("quantity");
        b.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(12, 2);
        b.Property(x => x.Currency).HasColumnName("currency").HasMaxLength(3).IsRequired();
        b.Property(x => x.ConfirmedAt).HasColumnName("confirmed_at").HasColumnType("timestamp with time zone");
        b.HasIndex(x => x.BookingCode).IsUnique();
        b.HasOne(x => x.Reservation).WithOne(x => x.Booking)
            .HasForeignKey<Booking>(x => x.ReservationId).OnDelete(DeleteBehavior.Restrict);
        b.HasOne(x => x.User).WithMany(x => x.Bookings)
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}