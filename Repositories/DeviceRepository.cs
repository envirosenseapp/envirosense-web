using System;
using EnviroSense.Web.Entities;
using Microsoft.EntityFrameworkCore;


namespace EnviroSense.Web.Repositories;

public class DeviceRepository : IDeciveRepository
{
    private readonly AppDbContext _context;

    public DeviceRepository(AppDbContext context)
    {
        _context = context;
    }

}
