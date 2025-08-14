
using EnviroSense.Domain.Entities;
using EnviroSense.Repositories.Repositories;


namespace EnviroSense.Plugins.PostgresRepositories.Repositories;

internal class AccountPasswordResetRepository : IAccountPasswordResetRepository
{
    public readonly AppDbContext _context;

    public AccountPasswordResetRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<AccountPasswordReset> CreateResetPasswordEntityAsync(AccountPasswordReset account)
    {
        var savedAccount = await _context.AccountPasswordResets.AddAsync(account);
        await _context.SaveChangesAsync();
        return savedAccount.Entity;
    }

    public Task<AccountPasswordReset> FetchAccountPasswordResetEntityByIdAsync(Guid securityCode)
    {
        var accountToReset = _context.AccountPasswordResets.FirstOrDefault(account => account.SecurityCode == securityCode);
        return Task.FromResult<AccountPasswordReset>(accountToReset);
    }

    public async Task<Account> UpdateAccountPasswordAsync(Guid accountId, string newPassword)
    {
        var UpdatedAccount = _context.Accounts.FirstOrDefault(account => account.Id == accountId);
        UpdatedAccount.Password = newPassword;
        await _context.SaveChangesAsync();
        return UpdatedAccount;
    }

    public async Task<AccountPasswordReset> SetUsedAtTimeAsync(Guid accountId)
    {
        var updatedAccount = _context.AccountPasswordResets.FirstOrDefault(account => account.AccountId == accountId);
        updatedAccount.UsedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return updatedAccount;
    }


}
