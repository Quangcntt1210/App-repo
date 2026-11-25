namespace APP.Entities;

public class Reservation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public Guid TicketTypeId { get; set; }
    public int Quantity { get; set; } = 1;
    public ReservationStatus Status { get; set; } = ReservationStatus.Held;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public DateTime? ConfirmedAt { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Event Event { get; set; } = null!;
    public TicketType TicketType { get; set; } = null!;
    public Booking? Booking { get; set; }
    public ICollection<BookingAuditEvent> AuditEvents { get; set; } = new List<BookingAuditEvent>();
}