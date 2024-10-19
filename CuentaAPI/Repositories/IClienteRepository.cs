using CuentaAPI.Models;

namespace CuentaAPI.Repositories;

public interface IClienteRepository
{
    Task<Guid> CreateAsync(Cliente cliente);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, Cliente cliente);
}
