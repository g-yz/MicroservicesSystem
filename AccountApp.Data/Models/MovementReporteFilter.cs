namespace AccountApp.Data.Models;

public class MovementReporteFilter
{
    public Guid? ClientId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? AccountNumber { get; set; }
}
