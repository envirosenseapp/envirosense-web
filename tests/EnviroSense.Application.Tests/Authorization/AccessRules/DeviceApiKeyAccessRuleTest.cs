using EnviroSense.Application.Authentication;
using EnviroSense.Application.Authorization.AccessRules;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using Moq;

namespace EnviroSense.Application.Tests.Authorization.AccessRules;

public class DeviceApiKeyAccessRuleTest : IDisposable
{
    private readonly Mock<IAuthenticationContext> _authenticationContext;
    private readonly DeviceApiKeyAccessRule _accessRule;

    public DeviceApiKeyAccessRuleTest()
    {
        _authenticationContext = new Mock<IAuthenticationContext>();
        _accessRule = new DeviceApiKeyAccessRule(_authenticationContext.Object);
    }

    [Fact]
    public async Task HasAccess_ShouldCompleteSuccessfully()
    {
        // Arrange
        var apiKey = SampleDeviceApiKeys().First();

        _authenticationContext.Setup(s => s.CurrentAccountId())
            .ReturnsAsync(SampleGuid());

        // Act
        var result = await _accessRule.HasAccess(apiKey);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task HasAccess_ShouldFailWhenAccountIDDoesNotMatch()
    {
        // Arrange
        var apiKey = SampleDeviceApiKeys().First();

        _authenticationContext.Setup(s => s.CurrentAccountId())
            .ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _accessRule.HasAccess(apiKey);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task HasAccess_ShouldThrowWhenAccountIDIsNotFound()
    {
        // Arrange
        var apiKey = SampleDeviceApiKeys().First();

        _authenticationContext.Setup(s => s.CurrentAccountId())
            .ReturnsAsync(Utils.Null<Guid?>());

        // Act
        await Assert.ThrowsAsync<AccessToForbiddenEntityException>(async () => await _accessRule.HasAccess(apiKey));
    }

    public void Dispose()
    {
        _authenticationContext.VerifyAll();
    }

    private Guid SampleGuid()
    {
        return Guid.Parse("5fe7be6c-4ce6-43ce-94f5-e4f91df55a74");
    }

    private List<ApiKey> SampleDeviceApiKeys()
    {
        var accountId = SampleGuid();

        var account = new Account
        {
            Id = accountId,
            Email = "test@test.com",
            Password = "1234" 
        };

        return new List<ApiKey>
        {
            new ApiKey
            {
                Id = Guid.NewGuid(),
                Name = "Test Key 1",
                KeyHash = "hash1",
                AccountId = accountId,
                Account = account   
            },
            new ApiKey
            {
                Id = Guid.NewGuid(),
                Name = "Test Key 2",
                KeyHash = "hash2",
                DisabledAt = DateTime.UtcNow,
                AccountId = accountId,
                Account = account   
            }
        };
    }
}
