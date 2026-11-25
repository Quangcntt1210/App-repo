using Microsoft.AspNetCore.Identity;

namespace APP.Entities;

// Identity manages password hash, username, roles and lockout.
public class ApplicationUser : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<IdempotencyRequest> IdempotencyRequests { get; set; } = new List<IdempotencyRequest>();
}