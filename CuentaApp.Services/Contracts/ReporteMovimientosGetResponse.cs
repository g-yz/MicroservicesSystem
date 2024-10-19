namespace CuentaApp.Services.Contracts;

public class ReporteMovimientosGetResponse
{
    public required string Fecha { get; set; }
    public required string Nombre { get; set; }
    public required string NumeroCuenta { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal Movimiento { get; set; }
    public decimal SaldoDisponible { get; set; }
    public bool Estado { get; set; }
}
