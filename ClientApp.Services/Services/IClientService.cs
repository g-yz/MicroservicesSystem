using ClientApp.Services.Contracts;

namespace ClientApp.Services.Services;

public interface IClientService
{
    Task<Guid> CreateAsync(ClientCreateRequest clientRequest);
    Task<bool> DeleteAsync(Guid id);
    Task<ClientGetResponse> GetAsync(Guid id);
    Task<IEnumerable<ClientGetResponse>> ListAsync();
    Task<bool> UpdateAsync(Guid id, ClientUpdateRequest clientUpdateRequest);
}
