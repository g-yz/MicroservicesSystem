using ClienteAPI.Contracts;

namespace ClienteAPI.Services;

public interface IClienteService
{
    Task<Guid> CreateAsync(ClienteCreateRequest clienteRequest);
    Task<bool> DeleteAsync(Guid id);
    Task<ClienteGetResponse> GetAsync(Guid id);
    Task<IEnumerable<ClienteGetResponse>> ListAsync();
    Task<bool> UpdateAsync(Guid id, ClienteUpdateRequest clienteUpdateRequest);
}
