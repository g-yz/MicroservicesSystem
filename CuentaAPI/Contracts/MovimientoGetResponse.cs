using CuentaAPI.Models;

namespace CuentaAPI.Contracts;

public class MovimientoGetResponse
{
    public Guid Id { get; set; }

    public required string Fecha { get; set; }

    public required string NumeroCuenta { get; set; }

    public required string Tipo { get; set; }

    public decimal SaldoInicial { get; set; }

    public required string Movimiento { get; set; }

    public bool Estado { get; set; }
}
