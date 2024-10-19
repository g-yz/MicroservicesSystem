using CuentaApp.Data.Models;
using CuentaApp.Services.Contracts;

namespace CuentaApp.Services.Services;

public interface IMovimientoService
{
    Task<Guid> CreateAsync(MovimientoAddRequest movimientoAddRequest);
    Task<IEnumerable<ReporteMovimientosGetResponse>> GetReporteAsync(MovimientoReporteFilter filters);
    Task<IEnumerable<MovimientoGetResponse>> ListAsync();
}
