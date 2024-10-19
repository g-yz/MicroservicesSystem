namespace CuentaAPI.Commands;

public interface IClienteCommandRequest
{
    Task<bool> VerificarClienteAsync(Guid id);
}
