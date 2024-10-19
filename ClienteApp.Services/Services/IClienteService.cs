using ClienteApp.Services.Contracts;

namespace ClienteApp.Services.Services;

public interface IClienteService
{
    Task<Guid> CreateAsync(ClienteCreateRequest clienteRequest);
    Task<bool> DeleteAsync(Guid id);
    Task<ClienteGetResponse> GetAsync(Guid id);
    Task<IEnumerable<ClienteGetResponse>> ListAsync();
    Task<bool> UpdateAsync(Guid id, ClienteUpdateRequest clienteUpdateRequest);
}
