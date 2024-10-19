using AutoMapper;
using ClienteApp.Data.Models;
using ClienteApp.Data.Repositories;
using ClienteApp.Messaging.Producers;
using ClienteApp.Services.Contracts;
using FluentValidation;
using SystemApp.Shared.Exceptions;

namespace ClienteApp.Services.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ClienteCreateRequest> _createValidator;
    private readonly IValidator<ClienteUpdateRequest> _updateValidator;
    private readonly IClienteEventPublisher _clientEventPublisher;

    public ClienteService(IClienteRepository clienteRepository,
        IMapper mapper,
        IValidator<ClienteCreateRequest> createValidator,
        IValidator<ClienteUpdateRequest> updateValidator,
        IClienteEventPublisher clientEventPublisher)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _clientEventPublisher = clientEventPublisher;
    }

    public async Task<Guid> CreateAsync(ClienteCreateRequest clienteRequest)
    {
        var result = _createValidator.Validate(clienteRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var cliente = _mapper.Map<Cliente>(clienteRequest);
        var nuevoCliente = await _clienteRepository.CreateAsync(cliente);

        _clientEventPublisher.PublicarClienteCreado(nuevoCliente);

        return nuevoCliente.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var status = await _clienteRepository.DeleteAsync(id);
        if (!status) throw new NotFoundException($"El cliente {id} no existe.");
        _clientEventPublisher.PublicarClienteEliminado(id);
        return status;
    }

    public async Task<ClienteGetResponse> GetAsync(Guid id)
    {
        var cliente = await _clienteRepository.GetAsync(id);
        if (cliente == null) throw new NotFoundException($"El cliente {id} no existe.");

        return _mapper.Map<ClienteGetResponse>(cliente);
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
        if (cliente == null) throw new NotFoundException($"El cliente {id} no existe.");

        _mapper.Map(clienteUpdateRequest, cliente);
        var status = await _clienteRepository.UpdateAsync(id, cliente);

        if (status) _clientEventPublisher.PublicarClienteModificado(cliente);

        return status;
    }
}
