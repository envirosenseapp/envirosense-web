using EnviroSense.Application.Authentication;
using EnviroSense.Application.Authorization.AccessRules;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using Moq;

namespace EnviroSense.Application.Tests.Authorization.AccessRules;

public class DeviceAccessRuleTest : IDisposable
{
    private readonly Mock<IAuthenticationRetriever> _authenticationRetriever;
    private readonly DeviceAccessRule _accessRule;

    public DeviceAccessRuleTest()
    {
        _authenticationRetriever = new Mock<IAuthenticationRetriever>();
        _accessRule = new DeviceAccessRule(_authenticationRetriever.Object);
    }

    [Fact]
    public async Task HasAccess_ShouldCompleteSuccessfully()
    {
        // Arrange
        var device = SampleDevice();

        _authenticationRetriever.Setup(s => s.GetCurrentAccountId())
            .ReturnsAsync(SampleGuid());

        // Act
        var result = await _accessRule.HasAccess(device);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task HasAccess_ShouldFailWhenAccountIDDoesNotMatch()
    {
        // Arrange
        var device = SampleDevice();

        _authenticationRetriever.Setup(s => s.GetCurrentAccountId())
            .ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _accessRule.HasAccess(device);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task HasAccess_ShouldThrowWhenAccountIDIsNotFound()
    {
        // Arrange
        var device = SampleDevice();

        _authenticationRetriever.Setup(s => s.GetCurrentAccountId())
            .ReturnsAsync(Utils.Null<Guid?>());

        // Act
        await Assert.ThrowsAsync<AccessToForbiddenEntityException>(async () => await _accessRule.HasAccess(device));
    }

    public void Dispose()
    {
        _authenticationRetriever.VerifyAll();
    }

    private Guid SampleGuid()
    {
        return Guid.Parse("5fe7be6c-4ce6-43ce-94f5-e4f91df55a74");
    }

    private Device SampleDevice()
    {
        return new Device
        {
            Id = Guid.NewGuid(),
            Name = "Test Device",
            AccountId = SampleGuid(),
            Account = new Account
            {
                Email = "a@a.com",
                Password = "p"
            },
        };
    }
}
