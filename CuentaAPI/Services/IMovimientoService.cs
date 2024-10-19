using CuentaAPI.Contracts;

namespace CuentaAPI.Services;

public interface IMovimientoService
{
    Task<Guid> CreateAsync(MovimientoAddRequest movimientoAddRequest);
    Task<IEnumerable<ReporteMovimientosGetResponse>> GetReporteAsync(MovimientoReporteFilter filters);
    Task<IEnumerable<MovimientoGetResponse>> ListAsync();
}
