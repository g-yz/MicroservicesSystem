using AutoMapper;
using ClientApp.Data.Models;
using ClientApp.Data.Repositories;
using ClientApp.Messaging.Producers;
using ClientApp.Services.Contracts;
using FluentValidation;
using Shared.Exceptions;

namespace ClientApp.Services.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ClientCreateRequest> _createValidator;
    private readonly IValidator<ClientUpdateRequest> _updateValidator;
    private readonly IClientEventPublisher _clientEventPublisher;

    public ClientService(IClientRepository clientRepository,
        IMapper mapper,
        IValidator<ClientCreateRequest> createValidator,
        IValidator<ClientUpdateRequest> updateValidator,
        IClientEventPublisher clientEventPublisher)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _clientEventPublisher = clientEventPublisher;
    }

    public async Task<Guid> CreateAsync(ClientCreateRequest clientRequest)
    {
        var result = _createValidator.Validate(clientRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var client = _mapper.Map<Client>(clientRequest);
        var nuevoCliente = await _clientRepository.CreateAsync(client);

        _clientEventPublisher.PublicarClienteCreado(nuevoCliente);

        return nuevoCliente.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var status = await _clientRepository.DeleteAsync(id);
        if (!status) throw new NotFoundException($"The client {id} does not exist.");
        _clientEventPublisher.PublicarClienteEliminado(id);
        return status;
    }

    public async Task<ClientGetResponse> GetAsync(Guid id)
    {
        var client = await _clientRepository.GetAsync(id);
        if (client == null) throw new NotFoundException($"The client {id} does not exist.");

        return _mapper.Map<ClientGetResponse>(client);
    }

    public async Task<IEnumerable<ClientGetResponse>> ListAsync()
    {
        return _mapper.Map<List<ClientGetResponse>>(await _clientRepository.ListAsync());
    }

    public async Task<bool> UpdateAsync(Guid id, ClientUpdateRequest clientUpdateRequest)
    {
        var result = _updateValidator.Validate(clientUpdateRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var client = await _clientRepository.GetAsync(id);
        if (client == null) throw new NotFoundException($"The client {id} does not exist.");

        _mapper.Map(clientUpdateRequest, client);
        var status = await _clientRepository.UpdateAsync(id, client);

        if (status) _clientEventPublisher.PublicarClienteModificado(client);

        return status;
    }
}
