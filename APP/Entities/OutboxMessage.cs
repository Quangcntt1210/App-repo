namespace APP.Entities;

public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string EventType { get; set; } = string.Empty;
    public Guid AggregateId { get; set; }
    // JSON serialized by the application, mapped to PostgreSQL jsonb.
    public string Payload { get; set; } = "{}";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; set; }
    public int AttemptCount { get; set; }
    public string? LastError { get; set; }
}