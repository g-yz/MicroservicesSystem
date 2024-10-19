using AccountApp.Services.Contracts;

namespace AccountApp.Services.Services;

public interface IAccountService
{
    Task<Guid> CreateAsync(AccountCreateRequest accountCreateRequest);
    Task<bool> DeleteAsync(Guid id);
    Task<int> DesactivarAccountsByClienteAsync(Guid id);
    Task<AccountGetResponse> GetAsync(Guid id);
    Task<IEnumerable<AccountGetResponse>> ListAsync();
    Task<bool> UpdateAsync(Guid id, AccountUpdateRequest accountUpdateRequest);
}
