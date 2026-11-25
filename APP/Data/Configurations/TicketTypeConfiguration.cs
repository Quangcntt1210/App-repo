using APP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Configurations;

public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> b)
    {
        b.ToTable("ticket_types", t => t.HasCheckConstraint("ck_ticket_types_price", "price >= 0"));
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.EventId).HasColumnName("event_id");
        b.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        b.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        b.Property(x => x.Price).HasColumnName("price").HasPrecision(12, 2);
        b.Property(x => x.Currency).HasColumnName("currency").HasMaxLength(3).IsRequired();
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone");
        b.HasIndex(x => new { x.EventId, x.Code }).IsUnique();
        // Allows Reservation to refer to EventId+TicketTypeId as one consistent pair.
        b.HasAlternateKey(x => new { x.EventId, x.Id });
        b.HasOne(x => x.Event).WithMany(x => x.TicketTypes)
            .HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.Restrict);
    }
}