using CuentaApp.Data.Models;

namespace CuentaApp.Data.Repositories;

public interface IClienteRepository
{
    Task<Guid> CreateAsync(Cliente cliente);
    Task<bool> DeleteAsync(Guid id);
    Task<Cliente?> GetAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, Cliente cliente);
}
