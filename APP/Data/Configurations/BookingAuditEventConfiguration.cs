using APP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Configurations;

public class BookingAuditEventConfiguration : IEntityTypeConfiguration<BookingAuditEvent>
{
    public void Configure(EntityTypeBuilder<BookingAuditEvent> b)
    {
        b.ToTable("booking_audit_events");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.ReservationId).HasColumnName("reservation_id");
        b.Property(x => x.ActorUserId).HasColumnName("actor_user_id");
        b.Property(x => x.Action).HasColumnName("action").HasMaxLength(80).IsRequired();
        b.Property(x => x.PreviousStatus).HasColumnName("previous_status").HasMaxLength(20);
        b.Property(x => x.NewStatus).HasColumnName("new_status").HasMaxLength(20);
        b.Property(x => x.Metadata).HasColumnName("metadata").HasColumnType("jsonb");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone");
        b.HasIndex(x => new { x.ReservationId, x.CreatedAt });
        b.HasOne(x => x.Reservation).WithMany(x => x.AuditEvents)
            .HasForeignKey(x => x.ReservationId).OnDelete(DeleteBehavior.Restrict);
        // ActorUserId is an audit identifier, not a user FK: keep history after account removal.
    }
}