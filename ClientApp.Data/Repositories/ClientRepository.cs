using ClientApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientApp.Data.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ClientDbContext _context;
    public ClientRepository(ClientDbContext context)
    {
        _context = context;
    }
    public async Task<Client> CreateAsync(Client client)
    {
        var nuevoCliente = await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return nuevoCliente.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) return false;
        client.Status = false;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Client?> GetAsync(Guid id)
    {
        return await _context.Clients
            .Include(x => x.TypeGender)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _context.Clients
            .Include(x => x.TypeGender)
            .Where(x => x.Status)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, Client client)
    {
        _context.Update(client);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}
