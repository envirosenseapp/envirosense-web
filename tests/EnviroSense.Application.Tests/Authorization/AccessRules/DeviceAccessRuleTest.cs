using EnviroSense.Application.Authorization.AccessRules;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using Moq;

namespace EnviroSense.Application.Tests.Authorization.AccessRules;

public class DeviceAccessRuleTest : IDisposable
{
    private readonly Mock<IAccountService> _accountService;
    private readonly DeviceAccessRule _accessRule;

    public DeviceAccessRuleTest()
    {
        _accountService = new Mock<IAccountService>();
        _accessRule = new DeviceAccessRule(_accountService.Object);
    }

    [Fact]
    public async Task HasAccess_ShouldCompleteSuccessfully()
    {
        // Arrange
        var device = SampleDevice();

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns(SampleGuid());

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

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns(Guid.NewGuid());

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

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns<Guid?>(null);

        // Act
        await Assert.ThrowsAsync<AccessToForbiddenEntityException>(async () => await _accessRule.HasAccess(device));
    }

    public void Dispose()
    {
        _accountService.VerifyAll();
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
