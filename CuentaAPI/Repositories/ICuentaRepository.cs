using CuentaAPI.Models;

namespace CuentaAPI.Repositories;

public interface ICuentaRepository
{
    Task<Guid> CreateAsync(Cuenta cuenta);
    Task<bool> DeleteAsync(Guid guid);
    Task<Cuenta?> GetAsync(Guid id);
    Task<IEnumerable<Cuenta>> ListAsync();
    Task<int> DesactivarCuentasByClienteAsync(Guid id);
    Task<bool> UpdateAsync(Guid guid, Cuenta cuenta);
}
