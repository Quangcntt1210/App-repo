namespace APP.Entities;

public class BookingAuditEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ReservationId { get; set; }
    public Guid? ActorUserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? PreviousStatus { get; set; }
    public string? NewStatus { get; set; }
    // JSON serialized by the application, never include secrets.
    public string? Metadata { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Reservation Reservation { get; set; } = null!;
}