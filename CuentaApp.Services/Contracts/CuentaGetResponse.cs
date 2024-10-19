namespace CuentaApp.Services.Contracts;

public class CuentaGetResponse
{
    public Guid Id { get; set; }
    public required string NumeroCuenta { get; set; }

    public decimal SaldoInicial { get; set; }

    public required string TipoCuenta { get; set; }

    public bool Estado { get; set; }

    public Guid ClienteId { get; set; }
}
