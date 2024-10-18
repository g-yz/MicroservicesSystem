using AutoMapper;
using ClienteAPI.Contracts;
using ClienteAPI.Models;
using ClienteAPI.Repositories;

namespace ClienteAPI.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }
    public async Task<Guid> CreateAsync(ClienteCreateRequest clienteRequest)
    {
        var cliente = _mapper.Map<Cliente>(clienteRequest);
        return await _clienteRepository.CreateAsync(cliente);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _clienteRepository.DeleteAsync(id);
    }

    public async Task<ClienteGetResponse> GetAsync(Guid id)
    {
        return _mapper.Map<ClienteGetResponse>(await _clienteRepository.GetAsync(id));
    }

    public async Task<IEnumerable<ClienteGetResponse>> ListAsync()
    {
        return _mapper.Map<List<ClienteGetResponse>>(await _clienteRepository.ListAsync());
    }

    public async Task<bool> UpdateAsync(Guid id, ClienteUpdateRequest clienteUpdateRequest)
    {
        var cliente = await _clienteRepository.GetAsync(id);
        if (cliente == null) return false;
        _mapper.Map(clienteUpdateRequest, cliente);
        return await _clienteRepository.UpdateAsync(id, cliente);
    }
}
