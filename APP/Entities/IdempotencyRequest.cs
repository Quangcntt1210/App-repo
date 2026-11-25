namespace APP.Entities;

public class IdempotencyRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string RequestHash { get; set; } = string.Empty;
    public IdempotencyStatus Status { get; set; } = IdempotencyStatus.Processing;
    public int? ResponseStatusCode { get; set; }
    public string? ResponseBody { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }

    public ApplicationUser User { get; set; } = null!;
}