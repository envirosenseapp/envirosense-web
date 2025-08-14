using System.Security.Cryptography;
using System.Text;

namespace EnviroSense.Application.Algorithms;

public class GuidApiKeyGenerator : IApiKeyGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }

    public string Hash(string key)
    {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(key));

        return Convert.ToBase64String(hash);
    }
}
