
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


}
