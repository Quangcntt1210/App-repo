using APP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Configurations;

public class TicketInventoryConfiguration : IEntityTypeConfiguration<TicketInventory>
{
    public void Configure(EntityTypeBuilder<TicketInventory> b)
    {
        b.ToTable("ticket_inventories", t => t.HasCheckConstraint("ck_inventory_valid",
            "capacity >= 0 AND available >= 0 AND held >= 0 AND sold >= 0 AND capacity = available + held + sold"));
        b.HasKey(x => x.TicketTypeId);
        b.Property(x => x.TicketTypeId).HasColumnName("ticket_type_id");
        b.Property(x => x.Capacity).HasColumnName("capacity");
        b.Property(x => x.Available).HasColumnName("available");
        b.Property(x => x.Held).HasColumnName("held");
        b.Property(x => x.Sold).HasColumnName("sold");
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp with time zone");
        b.HasOne(x => x.TicketType).WithOne(x => x.Inventory)
            .HasForeignKey<TicketInventory>(x => x.TicketTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}