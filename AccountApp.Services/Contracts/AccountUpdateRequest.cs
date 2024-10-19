namespace AccountApp.Services.Contracts;

public class AccountUpdateRequest
{
    public required string AccountNumber { get; set; }

    public decimal OpeningBalance { get; set; }

    public int TypeAccountId { get; set; }

    public bool Status { get; set; }

    public Guid ClientId { get; set; }
}
