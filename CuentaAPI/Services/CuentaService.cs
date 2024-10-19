﻿using AutoMapper;
using CuentaAPI.Contracts;
using CuentaAPI.Models;
using CuentaAPI.Repositories;
using FluentValidation;

namespace CuentaAPI.Services;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CuentaCreateRequest> _createValidator;
    private readonly IValidator<CuentaUpdateRequest> _updateValidator;
    private readonly IClienteService _clienteService;

    public CuentaService(ICuentaRepository repository, 
        IMapper mapper, 
        IValidator<CuentaCreateRequest> createValidator, 
        IValidator<CuentaUpdateRequest> updateValidator,
        IClienteService clienteService)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _clienteService = clienteService;
    }
    public async Task<Guid> CreateAsync(CuentaCreateRequest cuentaCreateRequest)
    {
        var result = _createValidator.Validate(cuentaCreateRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var clienteExiste = await _clienteService.VerificarClienteAsync(cuentaCreateRequest.ClienteId);
        if (!clienteExiste) throw new NotFoundException("El cliente no existe.");

        var cuenta = _mapper.Map<Cuenta>(cuentaCreateRequest);
        return await _repository.CreateAsync(cuenta);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<CuentaGetResponse> GetAsync(Guid id)
    {
        var cuenta = await _repository.GetAsync(id);
        if(cuenta == null) throw new NotFoundException("La cuenta no existe.");
        return _mapper.Map<CuentaGetResponse>(cuenta);
    }

    public async Task<IEnumerable<CuentaGetResponse>> ListAsync()
    {
        return _mapper.Map<List<CuentaGetResponse>>(await _repository.ListAsync());
    }

    public async Task<bool> UpdateAsync(Guid id, CuentaUpdateRequest cuentaUpdateRequest)
    {
        var result = _updateValidator.Validate(cuentaUpdateRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var cuenta = _mapper.Map<Cuenta>(cuentaUpdateRequest);
        return await _repository.UpdateAsync(id, cuenta);
    }
}
