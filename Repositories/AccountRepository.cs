using EnviroSense.Web.Entities;
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

}
