namespace CuentaApp.Services.Contracts;

public class MovimientoAddRequest
{
    public decimal Valor { get; set; }

    public Guid CuentaId { get; set; }
}
