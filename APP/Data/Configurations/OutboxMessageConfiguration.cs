using APP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> b)
    {
        b.ToTable("outbox_messages", t => t.HasCheckConstraint(
            "ck_outbox_attempt_count", "attempt_count >= 0"));
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.EventType).HasColumnName("event_type").HasMaxLength(150).IsRequired();
        b.Property(x => x.AggregateId).HasColumnName("aggregate_id");
        b.Property(x => x.Payload).HasColumnName("payload").HasColumnType("jsonb").IsRequired();
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone");
        b.Property(x => x.PublishedAt).HasColumnName("published_at").HasColumnType("timestamp with time zone");
        b.Property(x => x.AttemptCount).HasColumnName("attempt_count");
        b.Property(x => x.LastError).HasColumnName("last_error");
        b.HasIndex(x => new { x.PublishedAt, x.CreatedAt });
    }
}