﻿using CuentaApp.Services.Contracts;

namespace CuentaApp.Services.Services;

public interface ICuentaService
{
    Task<Guid> CreateAsync(CuentaCreateRequest cuentaCreateRequest);
    Task<bool> DeleteAsync(Guid id);
    Task<int> DesactivarCuentasByClienteAsync(Guid id);
    Task<CuentaGetResponse> GetAsync(Guid id);
    Task<IEnumerable<CuentaGetResponse>> ListAsync();
    Task<bool> UpdateAsync(Guid id, CuentaUpdateRequest cuentaUpdateRequest);
}