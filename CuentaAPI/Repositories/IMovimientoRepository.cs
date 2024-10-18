using CuentaAPI.Contracts;
using CuentaAPI.Models;

namespace CuentaAPI.Repositories;

public interface IMovimientoRepository
{
    Task<Guid> CreateAsync(Movimiento movimiento);
    Task<IEnumerable<Movimiento>> GetByCuentaAsync(Guid id);
    Task<IEnumerable<Movimiento>> GetReporteAsync(MovimientoReporteFilter filters);
}
