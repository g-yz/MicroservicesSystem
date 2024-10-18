﻿using ClienteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ClienteDbContext _context;
    public ClienteRepository(ClienteDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateAsync(Cliente cliente)
    {
        var nuevoCliente = await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return nuevoCliente.Entity.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if(cliente == null) return false;
        cliente.Estado = false;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Cliente?> GetAsync(Guid id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<IEnumerable<Cliente>> ListAsync()
    {
        return await _context.Clientes
            .Include(c => c.Persona)
            .Where(c => c.Estado)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, Cliente cliente)
    {
        _context.Update(cliente);
        var result = await _context.SaveChangesAsync(); 
        return result > 0;
    }
}