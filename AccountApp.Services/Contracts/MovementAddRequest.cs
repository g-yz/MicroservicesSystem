namespace AccountApp.Services.Contracts;

public class MovementAddRequest
{
    public decimal Value { get; set; }

    public Guid AccountId { get; set; }
}
