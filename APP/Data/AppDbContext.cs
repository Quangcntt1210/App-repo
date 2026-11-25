using APP.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APP.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<TicketType> TicketTypes => Set<TicketType>();
    public DbSet<TicketInventory> TicketInventories => Set<TicketInventory>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<IdempotencyRequest> IdempotencyRequests => Set<IdempotencyRequest>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<BookingAuditEvent> BookingAuditEvents => Set<BookingAuditEvent>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Required for ASP.NET Core Identity keys/relations.

        builder.Entity<ApplicationUser>(user =>
        {
            user.Property(x => x.DisplayName).HasMaxLength(150).IsRequired();
            user.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone");
        });

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}