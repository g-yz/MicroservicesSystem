using AccountApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AccountApp.Data.Repositories;

public class MovementRepository : IMovementRepository
{
    private readonly AccountDbContext _context;
    public MovementRepository(AccountDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateAsync(Movement movement)
    {
        var nuevoMovement = await _context.Movements.AddAsync(movement);
        await _context.SaveChangesAsync();
        return nuevoMovement.Entity.Id;
    }

    public async Task<IEnumerable<Movement>> GetByAccountAsync(Guid id)
    {
        return await _context.Movements.Where(x => x.AccountId == id).ToListAsync();
    }

    public async Task<IEnumerable<Movement>> GetReporteAsync(MovementReporteFilter filters)
    {
        var query = _context.Movements
            .Include(x => x.Account)
            .Include(x => x.Account.Client)
            .AsQueryable();
        if (filters.ClientId.HasValue)
            query = query.Where(x => x.Account.ClientId == filters.ClientId.Value);
        if (filters.StartDate.HasValue)
            query = query.Where(x => x.Date >= filters.StartDate);
        if (filters.EndDate.HasValue)
            query = query.Where(x => x.Date <= filters.EndDate);
        if (!filters.AccountNumber.IsNullOrEmpty())
            query = query.Where(x => x.Account.AccountNumber == filters.AccountNumber);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Movement>> ListAsync()
    {
        return await _context.Movements
            .Include(x => x.Account)
            .Include(x => x.Account.TypeAccount)
            .Include(x => x.TypeMovement)
            .ToListAsync();
    }
}
