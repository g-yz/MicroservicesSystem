namespace AccountApp.Services.Contracts;

public class AccountGetResponse
{
    public Guid Id { get; set; }
    public required string AccountNumber { get; set; }

    public decimal OpeningBalance { get; set; }

    public required string TypeAccount { get; set; }

    public bool Status { get; set; }

    public Guid ClientId { get; set; }
}
