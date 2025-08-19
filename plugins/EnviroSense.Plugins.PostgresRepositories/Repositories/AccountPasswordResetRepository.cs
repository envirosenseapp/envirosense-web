using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;


namespace EnviroSense.Plugins.PostgresRepositories.Repositories;

internal class AccountPasswordResetRepository : IAccountPasswordResetRepository
{
    public readonly AppDbContext _context;

    public AccountPasswordResetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AccountPasswordReset> CreateAsync(AccountPasswordReset account)
    {
        var savedAccount = await _context.AccountPasswordResets.AddAsync(account);
        await _context.SaveChangesAsync();
        return savedAccount.Entity;
    }

    public async Task<AccountPasswordReset> GetBySecurityCodeAsync(Guid securityCode)
    {
        var reset = await _context.AccountPasswordResets.FirstOrDefaultAsync(account => account.SecurityCode == securityCode);
        if (reset == null)
        {
            throw new AccountPasswordResetNotFoundException();
        }

        return reset;
    }

    public async Task<AccountPasswordReset> UpdateAsync(AccountPasswordReset account)
    {
        var trackedEntity = _context.AccountPasswordResets.Update(account);
        await _context.SaveChangesAsync();
        return trackedEntity.Entity;
    }

    public async Task<Account> ResetPasswordFromSettingsAsync(Account account)
    {
        var updatedAccount = _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        return updatedAccount.Entity;
    }
}
