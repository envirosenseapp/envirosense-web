namespace EnviroSense.Application.Algorithms;

public interface IApiKeyGenerator
{
    string Generate();
    string Hash(string key);
}
