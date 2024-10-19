using AutoMapper;
using ClienteAPI.Contracts;
using ClienteAPI.Models;
using ClienteAPI.Repositories;
using FluentValidation;

namespace ClienteAPI.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ClienteCreateRequest> _createValidator;
    private readonly IValidator<ClienteUpdateRequest> _updateValidator;
    public ClienteService(IClienteRepository clienteRepository,
        IMapper mapper,
        IValidator<ClienteCreateRequest> createValidator,
        IValidator<ClienteUpdateRequest> updateValidator)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<Guid> CreateAsync(ClienteCreateRequest clienteRequest)
    {
        var result = _createValidator.Validate(clienteRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

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
        var result = _updateValidator.Validate(clienteUpdateRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var cliente = await _clienteRepository.GetAsync(id);
        if (cliente == null) return false;
        _mapper.Map(clienteUpdateRequest, cliente);
        return await _clienteRepository.UpdateAsync(id, cliente);
    }
}
