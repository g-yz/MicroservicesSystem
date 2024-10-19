using CuentaApp.Data.Models;

namespace CuentaApp.Data.Repositories;

public interface IMovimientoRepository
{
    Task<Guid> CreateAsync(Movimiento movimiento);
    Task<IEnumerable<Movimiento>> GetByCuentaAsync(Guid id);
    Task<IEnumerable<Movimiento>> GetReporteAsync(MovimientoReporteFilter filters);
    Task<IEnumerable<Movimiento>> ListAsync();
}
