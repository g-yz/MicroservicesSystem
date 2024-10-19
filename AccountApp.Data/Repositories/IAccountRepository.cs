using AccountApp.Data.Models;

namespace AccountApp.Data.Repositories;

public interface IAccountRepository
{
    Task<Guid> CreateAsync(Account account);
    Task<bool> DeleteAsync(Guid id);
    Task<Account?> GetAsync(Guid id);
    Task<IEnumerable<Account>> ListAsync();
    Task<int> DeleteByClienteAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, Account account);
}
