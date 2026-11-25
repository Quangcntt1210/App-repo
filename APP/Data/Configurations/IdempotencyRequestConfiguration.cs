using APP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Configurations;

public class IdempotencyRequestConfiguration : IEntityTypeConfiguration<IdempotencyRequest>
{
    public void Configure(EntityTypeBuilder<IdempotencyRequest> b)
    {
        b.ToTable("idempotency_requests");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.UserId).HasColumnName("user_id");
        b.Property(x => x.Operation).HasColumnName("operation").HasMaxLength(50).IsRequired();
        b.Property(x => x.Key).HasColumnName("key").HasMaxLength(128).IsRequired();
        b.Property(x => x.RequestHash).HasColumnName("request_hash").HasMaxLength(128).IsRequired();
        b.Property(x => x.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20).IsRequired();
        b.Property(x => x.ResponseStatusCode).HasColumnName("response_status_code");
        b.Property(x => x.ResponseBody).HasColumnName("response_body");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone");
        b.Property(x => x.ExpiresAt).HasColumnName("expires_at").HasColumnType("timestamp with time zone");
        b.HasIndex(x => new { x.UserId, x.Operation, x.Key }).IsUnique();
        b.HasIndex(x => x.ExpiresAt);
        b.HasOne(x => x.User).WithMany(x => x.IdempotencyRequests)
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}