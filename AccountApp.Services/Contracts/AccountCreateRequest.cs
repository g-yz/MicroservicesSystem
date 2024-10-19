namespace AccountApp.Services.Contracts;

public class AccountCreateRequest
{
    public required string AccountNumber { get; set; }

    public decimal OpeningBalance { get; set; }

    public int TypeAccountId { get; set; }

    public bool Status { get; set; } = true;

    public Guid ClientId { get; set; }
}
