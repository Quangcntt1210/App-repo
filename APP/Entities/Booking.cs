namespace APP.Entities;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
    public string BookingCode { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public string Currency { get; set; } = "VND";
    public DateTime ConfirmedAt { get; set; } = DateTime.UtcNow;

    public Reservation Reservation { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}