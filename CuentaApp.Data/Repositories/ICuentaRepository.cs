using CuentaApp.Data.Models;

namespace CuentaApp.Data.Repositories;

public interface ICuentaRepository
{
    Task<Guid> CreateAsync(Cuenta cuenta);
    Task<bool> DeleteAsync(Guid id);
    Task<Cuenta?> GetAsync(Guid id);
    Task<IEnumerable<Cuenta>> ListAsync();
    Task<int> DeleteByClienteAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, Cuenta cuenta);
}
