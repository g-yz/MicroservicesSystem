using AccountApp.Data.Models;

namespace AccountApp.Data.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AccountDbContext _context;
    public ClientRepository(AccountDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateAsync(Client client)
    {
        var nuevoCliente = await _context.AddAsync(client);
        await _context.SaveChangesAsync();
        return nuevoCliente.Entity.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) return false;
        client.Status = false;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Client    ?> GetAsync(Guid id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(Guid id, Client client)
    {
        var clientModificado = await _context.Clients.FindAsync(id);
        if (clientModificado == null) return false;
        clientModificado.FullName = client.FullName;
        clientModificado.Status = client.Status;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}
