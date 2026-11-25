using APP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> b)
    {
        b.ToTable("reservations", t => t.HasCheckConstraint(
            "ck_reservations_quantity", "quantity = 1")); // MVP: one ticket per reservation.
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.UserId).HasColumnName("user_id");
        b.Property(x => x.EventId).HasColumnName("event_id");
        b.Property(x => x.TicketTypeId).HasColumnName("ticket_type_id");
        b.Property(x => x.Quantity).HasColumnName("quantity");
        b.Property(x => x.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20).IsRequired();
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone");
        b.Property(x => x.ExpiresAt).HasColumnName("expires_at").HasColumnType("timestamp with time zone");
        b.Property(x => x.ConfirmedAt).HasColumnName("confirmed_at").HasColumnType("timestamp with time zone");

        b.HasOne(x => x.User).WithMany(x => x.Reservations)
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        b.HasOne(x => x.Event).WithMany(x => x.Reservations)
            .HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.Restrict);
        b.HasOne(x => x.TicketType).WithMany(x => x.Reservations)
            .HasForeignKey(x => new { x.EventId, x.TicketTypeId })
            .HasPrincipalKey(x => new { x.EventId, x.Id })
            .OnDelete(DeleteBehavior.Restrict);
        b.HasIndex(x => new { x.Status, x.ExpiresAt });
        b.HasIndex(x => new { x.UserId, x.CreatedAt });
        // One HELD or CONFIRMED reservation per user and event.
        b.HasIndex(x => new { x.UserId, x.EventId })
            .IsUnique().HasFilter("status IN ('Held', 'Confirmed')");
    }
}