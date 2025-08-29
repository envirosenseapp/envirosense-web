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
        var apiKey = SampleDeviceApiKey();

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
        var apiKey = SampleDeviceApiKey();

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
        var apiKey = SampleDeviceApiKey();

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

    private ApiKey SampleDeviceApiKey()
    {
        return new ApiKey()
        {
            Id = Guid.NewGuid(),
            Name = "Test Key",
            KeyHash = "",
            Account = new Account()
            {
                Email = "a@a.com",
                Password = "p"
            }
        };
    }
}
