using AccountApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountApp.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountDbContext _context;
    public AccountRepository(AccountDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateAsync(Account account)
    {
        var nuevaAccount = await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return nuevaAccount.Entity.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) return false;
        account.Status = false;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Account?> GetAsync(Guid id)
    {
        return await _context.Accounts
            .Include(x => x.TypeAccount)
            .Where(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Account>> ListAsync()
    {
        return await _context.Accounts
            .Include(x => x.TypeAccount)
            .Where(x => x.Status)
            .ToListAsync();
    }

    public async Task<int> DeleteByClienteAsync(Guid id)
    {
        var accounts = await _context.Accounts
            .Where(x => x.ClientId == id)
            .ToListAsync();
        accounts.ForEach(x => x.Status = false);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<bool> UpdateAsync(Guid id, Account account)
    {
        var accountModificada = await _context.Accounts.FindAsync(id);
        if (accountModificada == null) return false;
        accountModificada.AccountNumber = account.AccountNumber;
        accountModificada.OpeningBalance = account.OpeningBalance;
        accountModificada.TypeAccountId = account.TypeAccountId;
        accountModificada.Status = account.Status;
        accountModificada.ClientId = account.ClientId;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}
