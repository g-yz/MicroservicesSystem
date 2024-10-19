namespace AccountApp.Services.Contracts;

public class ReportOfMovementsGetResponse
{
    public required string Date { get; set; }
    public required string FullName { get; set; }
    public required string AccountNumber { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal Movement { get; set; }
    public decimal AvailableBalance { get; set; }
    public bool Status { get; set; }
}
