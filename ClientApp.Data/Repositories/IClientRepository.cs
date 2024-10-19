using ClientApp.Data.Models;

namespace ClientApp.Data.Repositories;

public interface IClientRepository
{
    Task<Client> CreateAsync(Client client);
    Task<bool> DeleteAsync(Guid id);
    Task<Client?> GetAsync(Guid id);
    Task<IEnumerable<Client>> ListAsync();
    Task<bool> UpdateAsync(Guid id, Client client);
}
