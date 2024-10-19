using AutoMapper;
using CuentaAPI.Contracts;
using CuentaAPI.Models;
using CuentaAPI.Repositories;
using FluentValidation;

namespace CuentaAPI.Services;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CuentaCreateRequest> _createValidator;
    private readonly IValidator<CuentaUpdateRequest> _updateValidator;

    public CuentaService(ICuentaRepository cuentaRepository,
        IClienteRepository clienteRepository,
        IMapper mapper, 
        IValidator<CuentaCreateRequest> createValidator, 
        IValidator<CuentaUpdateRequest> updateValidator)
    {
        _cuentaRepository = cuentaRepository;
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }
    public async Task<Guid> CreateAsync(CuentaCreateRequest cuentaCreateRequest)
    {
        var result = _createValidator.Validate(cuentaCreateRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);
        
        var clienteExiste = await _clienteRepository.GetAsync(cuentaCreateRequest.ClienteId);
        if (clienteExiste == null) throw new NotFoundException($"El cliente {cuentaCreateRequest.ClienteId} no existe.");

        var cuenta = _mapper.Map<Cuenta>(cuentaCreateRequest);
        return await _cuentaRepository.CreateAsync(cuenta);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _cuentaRepository.DeleteAsync(id);
        if (!result) throw new NotFoundException($"La cuenta {id} no existe.");

        return result;
    }

    public async Task<int> DesactivarCuentasByClienteAsync(Guid id)
    {
        return await _cuentaRepository.DesactivarCuentasByClienteAsync(id);
    }

    public async Task<CuentaGetResponse> GetAsync(Guid id)
    {
        var cuenta = await _cuentaRepository.GetAsync(id);
        if(cuenta == null) throw new NotFoundException($"La cuenta {id} no existe.");
        return _mapper.Map<CuentaGetResponse>(cuenta);
    }

    public async Task<IEnumerable<CuentaGetResponse>> ListAsync()
    {
        return _mapper.Map<List<CuentaGetResponse>>(await _cuentaRepository.ListAsync());
    }

    public async Task<bool> UpdateAsync(Guid id, CuentaUpdateRequest cuentaUpdateRequest)
    {
        var result = _updateValidator.Validate(cuentaUpdateRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var clienteExiste = await _clienteRepository.GetAsync(cuentaUpdateRequest.ClienteId);
        if (clienteExiste == null) throw new NotFoundException($"El cliente {cuentaUpdateRequest.ClienteId} no existe.");

        var cuenta = _mapper.Map<Cuenta>(cuentaUpdateRequest);
        var status = await _cuentaRepository.UpdateAsync(id, cuenta);
        if (!status) throw new NotFoundException($"La cuenta {id} no existe.");

        return status;
    }
}
