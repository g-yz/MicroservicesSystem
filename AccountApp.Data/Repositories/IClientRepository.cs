using AccountApp.Data.Models;

namespace AccountApp.Data.Repositories;

public interface IClientRepository
{
    Task<Guid> CreateAsync(Client client);
    Task<bool> DeleteAsync(Guid id);
    Task<Client?> GetAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, Client client);
}
