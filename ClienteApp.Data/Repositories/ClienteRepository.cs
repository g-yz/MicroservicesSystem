using ClienteAPI.Models;
using ClienteApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteApp.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ClienteDbContext _context;
    public ClienteRepository(ClienteDbContext context)
    {
        _context = context;
    }
    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        var nuevoCliente = await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return nuevoCliente.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return false;
        cliente.Estado = false;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Cliente?> GetAsync(Guid id)
    {
        return await _context.Clientes
            .Include(x => x.TipoGenero)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Cliente>> ListAsync()
    {
        return await _context.Clientes
            .Include(x => x.TipoGenero)
            .Where(x => x.Estado)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, Cliente cliente)
    {
        _context.Update(cliente);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}
