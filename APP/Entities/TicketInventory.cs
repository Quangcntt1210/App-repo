namespace APP.Entities;

public class TicketInventory
{
    // Shared primary key + foreign key to TicketType.
    public Guid TicketTypeId { get; set; }
    public int Capacity { get; set; }
    public int Available { get; set; }
    public int Held { get; set; }
    public int Sold { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public TicketType TicketType { get; set; } = null!;
}