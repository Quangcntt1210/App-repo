namespace APP.Entities;

public enum EventStatus { Draft, Published, Cancelled }
public enum ReservationStatus { Held, Confirmed, Expired, Cancelled }
public enum IdempotencyStatus { Processing, Completed, Failed }