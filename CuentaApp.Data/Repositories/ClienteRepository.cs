﻿using CuentaApp.Data.Models;

namespace CuentaApp.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly CuentaDbContext _context;
    public ClienteRepository(CuentaDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateAsync(Cliente cliente)
    {
        var nuevoCliente = await _context.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return nuevoCliente.Entity.Id;
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
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(Guid id, Cliente cliente)
    {
        var clienteModificado = await _context.Clientes.FindAsync(id);
        if (clienteModificado == null) return false;
        clienteModificado.Nombres = cliente.Nombres;
        clienteModificado.Estado = cliente.Estado;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}