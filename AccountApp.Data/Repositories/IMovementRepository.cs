using AccountApp.Data.Models;

namespace AccountApp.Data.Repositories;

public interface IMovementRepository
{
    Task<Guid> CreateAsync(Movement movement);
    Task<IEnumerable<Movement>> GetByAccountAsync(Guid id);
    Task<IEnumerable<Movement>> GetReporteAsync(MovementReporteFilter filters);
    Task<IEnumerable<Movement>> ListAsync();
}
