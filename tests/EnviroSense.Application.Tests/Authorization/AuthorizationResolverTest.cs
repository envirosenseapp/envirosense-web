using EnviroSense.Application.Authorization;
using EnviroSense.Application.Authorization.AccessRules;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace EnviroSense.Application.Tests.Authorization;

public class AuthorizationResolverTest: IDisposable
{
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly Mock<IAccessRule<Device>> _accessRuleMock;
    private readonly IAuthorizationResolver _resolver;

    public AuthorizationResolverTest()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _accessRuleMock = new Mock<IAccessRule<Device>>();
        _resolver = new AuthorizationResolver(_serviceProviderMock.Object);
    }

    [Fact]
    public async Task MustHaveAccess_WhenRuleExistsAndGrantsAccess_ShouldCompleteSuccessfully()
    {
        // Arrange
        var device = SampleDevice();

        _accessRuleMock.Setup(r => r.HasAccess(device))
            .ReturnsAsync(true);
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccessRule<Device>)))
            .Returns(_accessRuleMock.Object);

        // Act
        await _resolver.MustHaveAccess(device);
    }
    
    [Fact]
    public async Task MustHaveAccess_WhenRuleExistsAndGrantsAccess_ShouldThrowException()
    {
        // Arrange
        var device = SampleDevice();

        _accessRuleMock.Setup(r => r.HasAccess(device))
            .Returns(Task.FromResult(false));
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccessRule<Device>)))
            .Returns(_accessRuleMock.Object);
        
        // Act
        await Assert.ThrowsAsync<AccessToForbiddenEntityException>(async() => await _resolver.MustHaveAccess(device));
    }
    
    [Fact]
    public async Task MustHaveAccess_WhenRuleNotFound_ShouldThrowException()
    {
        // Arrange
        var device = SampleDevice();

        _serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccessRule<Device>)))
            .Throws(new Exception());

        // Act
        await Assert.ThrowsAsync<Exception>(async() => await _resolver.MustHaveAccess(device));
    }
    

    public void Dispose()
    {
        _serviceProviderMock.VerifyAll();
        _accessRuleMock.VerifyAll();
    }

    private Device SampleDevice()
    {
        return new Device
        {
            Id = Guid.NewGuid(),
            Name = "Test Device",
            Account = new Account
            {
                Email = "a@a.com",
                Password = "p"
            },
        };
    }
}
