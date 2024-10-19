namespace CuentaAPI.Services;

public interface IClienteService
{
    Task<bool> VerificarClienteAsync(Guid id);
}
