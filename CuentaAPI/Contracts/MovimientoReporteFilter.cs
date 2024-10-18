namespace CuentaAPI.Contracts;

public class MovimientoReporteFilter
{
    public Guid? ClienteId { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string? NumeroCuenta { get; set; }
}
