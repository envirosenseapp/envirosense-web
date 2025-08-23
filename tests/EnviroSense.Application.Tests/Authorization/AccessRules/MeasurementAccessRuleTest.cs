using EnviroSense.Application.Authorization.AccessRules;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using Moq;

namespace EnviroSense.Application.Tests.Authorization.AccessRules;

public class MeasurementAccessRuleTest : IDisposable
{
    private readonly Mock<IAccountService> _accountService;
    private readonly MeasurementAccessRule _accessRule;

    public MeasurementAccessRuleTest()
    {
        _accountService = new Mock<IAccountService>();
        _accessRule = new MeasurementAccessRule((Authentication.IAuthenticationRetriever)_accountService.Object);
    }

    [Fact]
    public async Task HasAccess_ShouldCompleteSuccessfully()
    {
        // Arrange
        var measurement = SampleMeasurement();

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns(SampleGuid());

        // Act
        var result = await _accessRule.HasAccess(measurement);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task HasAccess_ShouldFailWhenAccountIDDoesNotMatch()
    {
        // Arrange
        var measurement = SampleMeasurement();

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns(Guid.NewGuid());

        // Act
        var result = await _accessRule.HasAccess(measurement);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task HasAccess_ShouldThrowWhenAccountIDIsNotFound()
    {
        // Arrange
        var measurement = SampleMeasurement();

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns(Utils.Null<Guid?>());

        // Act
        await Assert.ThrowsAsync<AccessToForbiddenEntityException>(async () => await _accessRule.HasAccess(measurement));
    }

    public void Dispose()
    {
        _accountService.VerifyAll();
    }

    private Guid SampleGuid()
    {
        return Guid.Parse("5fe7be6c-4ce6-43ce-94f5-e4f91df55a74");
    }

    private Measurement SampleMeasurement()
    {
        return new Measurement()
        {
            Id = Guid.NewGuid(),
            RecordingDate = DateTime.Now,
            Device = new Device()
            {
                Name = "Test device",
                AccountId = SampleGuid(),
                Account = new Account
                {
                    Email = "a@a.com",
                    Password = "p"
                },
            }
        };
    }
}
