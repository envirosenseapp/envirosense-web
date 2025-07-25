using System;

namespace EnviroSense.Web.Repositories;

public class AccountRepository : IAccountRepository
{
    public readonly AppDbContext _context;
    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

}
