using EnviroSense.Domain.Entities;
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

    public Task<AccountPasswordReset> GetBySecurityCodeAsync(Guid securityCode)
    {
        var accountToReset =
            _context.AccountPasswordResets.FirstOrDefault(account => account.SecurityCode == securityCode);
        return Task.FromResult<AccountPasswordReset>(accountToReset);
    }

    public async Task<Account> UpdateAccountPasswordAsync(Guid accountId, string newPassword)
    {
        var updatedAccount = await _context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
        updatedAccount.Password = newPassword;
        var setUsedAt = await _context.AccountPasswordResets.FirstOrDefaultAsync(account => account.AccountId == accountId);
        setUsedAt.UsedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return updatedAccount;
    }
}
