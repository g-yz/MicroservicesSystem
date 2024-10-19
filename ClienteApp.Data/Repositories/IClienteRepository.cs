﻿using ClienteApp.Data.Models;

namespace ClienteApp.Data.Repositories;

public interface IClienteRepository
{
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<bool> DeleteAsync(Guid id);
    Task<Cliente?> GetAsync(Guid id);
    Task<IEnumerable<Cliente>> ListAsync();
    Task<bool> UpdateAsync(Guid id, Cliente cliente);
}