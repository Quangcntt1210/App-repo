using APP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> b)
    {
        b.ToTable("events", t => t.HasCheckConstraint(
            "ck_events_sale_window", "sale_ends_at > sale_starts_at"));
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        b.Property(x => x.Slug).HasColumnName("slug").HasMaxLength(250).IsRequired();
        b.Property(x => x.Description).HasColumnName("description");
        b.Property(x => x.Location).HasColumnName("location").HasMaxLength(255);
        b.Property(x => x.StartsAt).HasColumnName("starts_at").HasColumnType("timestamp with time zone");
        b.Property(x => x.SaleStartsAt).HasColumnName("sale_starts_at").HasColumnType("timestamp with time zone");
        b.Property(x => x.SaleEndsAt).HasColumnName("sale_ends_at").HasColumnType("timestamp with time zone");
        b.Property(x => x.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20).IsRequired();
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone");
        b.HasIndex(x => x.Slug).IsUnique();
    }
}