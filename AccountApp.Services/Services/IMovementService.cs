using AccountApp.Data.Models;
using AccountApp.Services.Contracts;

namespace AccountApp.Services.Services;

public interface IMovementService
{
    Task<Guid> CreateAsync(MovementAddRequest movementAddRequest);
    Task<IEnumerable<ReportOfMovementsGetResponse>> GetReporteAsync(MovementReporteFilter filters);
    Task<IEnumerable<MovementGetResponse>> ListAsync();
}
