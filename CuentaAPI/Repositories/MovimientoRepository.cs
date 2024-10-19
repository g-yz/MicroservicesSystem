using CuentaAPI.Contracts;
using CuentaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CuentaAPI.Repositories;

public class MovimientoRepository : IMovimientoRepository
{
    private readonly CuentaDbContext _context;
    public MovimientoRepository(CuentaDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateAsync(Movimiento movimiento)
    {
        var nuevoMovimiento = await _context.Movimientos.AddAsync(movimiento);
        await _context.SaveChangesAsync();
        return nuevoMovimiento.Entity.Id;
    }

    public async Task<IEnumerable<Movimiento>> GetByCuentaAsync(Guid id)
    {
        return await _context.Movimientos.Where(x => x.CuentaId == id).ToListAsync();
    }

    public async Task<IEnumerable<Movimiento>> GetReporteAsync(MovimientoReporteFilter filters)
    {
        var query = _context.Movimientos
            .Include(x => x.Cuenta)
            .Include(x => x.Cuenta.Cliente)
            .AsQueryable();
        if(filters.ClienteId.HasValue)
            query = query.Where(x => x.Cuenta.ClienteId == filters.ClienteId.Value);
        if (filters.FechaInicio.HasValue)
            query = query.Where(x => x.Fecha >= filters.FechaInicio);
        if(filters.FechaFin.HasValue)
            query = query.Where(x => x.Fecha <= filters.FechaFin);
        if (!filters.NumeroCuenta.IsNullOrEmpty())
            query = query.Where(x => x.Cuenta.NumeroCuenta == filters.NumeroCuenta);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Movimiento>> ListAsync()
    {
        return await _context.Movimientos
            .Include(x => x.Cuenta)
            .Include(x => x.Cuenta.TipoCuenta)
            .Include(x => x.TipoMovimiento)
            .ToListAsync();
    }
}
