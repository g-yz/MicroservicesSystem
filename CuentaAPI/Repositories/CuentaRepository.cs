using CuentaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CuentaAPI.Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly CuentaDbContext _context;
    public CuentaRepository(CuentaDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateAsync(Cuenta cuenta)
    {
        var nuevaCuenta = await _context.Cuentas.AddAsync(cuenta);
        await _context.SaveChangesAsync();
        return nuevaCuenta.Entity.Id;
    }

    public async Task<bool> DeleteAsync(Guid guid)
    {
        var cuenta = await _context.Cuentas.FindAsync(guid);
        if (cuenta == null) return false;
        cuenta.Estado = false;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Cuenta?> GetAsync(Guid id)
    {
        return await _context.Cuentas
            .Include(x => x.TipoCuenta)
            .Where(x => x.Estado)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Cuenta>> ListAsync()
    {
        return await _context.Cuentas
            .Include(x => x.TipoCuenta)
            .Where(x => x.Estado)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(Guid guid, Cuenta cuenta)
    {
        var cuentaModificada = await _context.Cuentas.FindAsync(guid);
        if(cuentaModificada == null) return false;
        cuentaModificada.NumeroCuenta = cuenta.NumeroCuenta;
        cuentaModificada.SaldoInicial = cuenta.SaldoInicial;
        cuentaModificada.TipoCuentaId = cuenta.TipoCuentaId;
        cuentaModificada.Estado = cuenta.Estado;
        cuentaModificada.ClienteId = cuenta.ClienteId;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}
