using System;
using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Services;

public interface IAccountService
{
    Task<bool> Validate(string email);
    Task<Account> Add(string email, string password);

}
