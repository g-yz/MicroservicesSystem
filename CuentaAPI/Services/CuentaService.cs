using AutoMapper;
using CuentaAPI.Contracts;
using CuentaAPI.Models;
using CuentaAPI.Repositories;

namespace CuentaAPI.Services;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _repository;
    private readonly IMapper _mapper;
    public CuentaService(ICuentaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Guid> CreateAsync(CuentaCreateRequest cuentaCreateRequest)
    {
        var cuenta = _mapper.Map<Cuenta>(cuentaCreateRequest);
        return await _repository.CreateAsync(cuenta);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<CuentaGetResponse?> GetAsync(Guid id)
    {
        return _mapper.Map<CuentaGetResponse>(await _repository.GetAsync(id));
    }

    public async Task<IEnumerable<CuentaGetResponse>> ListAsync()
    {
        return _mapper.Map<List<CuentaGetResponse>>(await _repository.ListAsync());
    }

    public async Task<bool> UpdateAsync(Guid id, CuentaUpdateRequest cuentaUpdateRequest)
    {
        var cuenta = _mapper.Map<Cuenta>(cuentaUpdateRequest);
        return await _repository.UpdateAsync(id, cuenta);
    }
}
