namespace APP.Entities;

public class TicketType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "VND";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Event Event { get; set; } = null!;
    public TicketInventory? Inventory { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}