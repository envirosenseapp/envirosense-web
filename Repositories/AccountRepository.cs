using EnviroSense.Web.Entities;
using EnviroSense.Web.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Web.Repositories;

public class AccountRepository : IAccountRepository
{
    public readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        var result = await _context.Accounts.AnyAsync(a => a.Email == email);
        return result;
    }

    public async Task<Account> AddAsync(Account account)
    {
        var createdAccount = await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        return createdAccount.Entity;
    }

    public async Task<Account> GetAccountByEmail(string email)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);

        if (account == null)
        {
            throw new AccountNotFoundException();
        }

        return account;
    }

    public async Task<Account> GetAccountByIdAsync(Guid accountId)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

        if (account == null)
        {
            throw new AccountNotFoundException();
        }

        return account;
    }
}
