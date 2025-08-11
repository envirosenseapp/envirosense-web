using EnviroSense.Domain.Entities;
using EnviroSense.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnviroSense.Plugins.PostgresRepositories.Repositories;

public class AccessRepository : IAccessRepository
{
    private readonly AppDbContext _context;
    public AccessRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Access> CreateAsync(Access access)
    {
        var createdAccess = await _context.Accesses.AddAsync(access);
        await _context.SaveChangesAsync();

        return createdAccess.Entity;
    }
    public Task<int> Count()
    {
        return _context.Accesses.CountAsync();
    }
    public async Task<List<Access>> ListAsync()
    {
        return await _context.Accesses.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }
}
