namespace AccountApp.Services.Contracts;

public class MovementGetResponse
{
    public Guid Id { get; set; }

    public required string Date { get; set; }

    public required string AccountNumber { get; set; }

    public required string Type { get; set; }

    public decimal OpeningBalance { get; set; }

    public required string Movement { get; set; }

    public bool Status { get; set; }
}
