namespace APP.Entities;

public class Event
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime SaleStartsAt { get; set; }
    public DateTime SaleEndsAt { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}