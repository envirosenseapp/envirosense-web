// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using EnviroSense.Application.Authorization.AccessRules;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using Moq;

namespace EnviroSense.Application.Tests.Authorization.AccessRules;

public class DeviceApiKeyAccessRuleTest: IDisposable
{
    private readonly Mock<IAccountService> _accountService;
    private readonly DeviceApiKeyAccessRule _accessRule;

    public DeviceApiKeyAccessRuleTest()
    {
        _accountService = new Mock<IAccountService>();
        _accessRule = new DeviceApiKeyAccessRule(_accountService.Object);
    }
    
    [Fact]
    public async Task HasAccess_ShouldCompleteSuccessfully()
    {
        // Arrange
        var apiKey = SampleDeviceApiKey();

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns(SampleGuid());
        
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

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns(Guid.NewGuid());

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

        _accountService.Setup(s => s.GetAccountIdFromSession())
            .Returns<Guid?>(null);

        // Act
        await Assert.ThrowsAsync<AccessToForbiddenEntityException>(async () => await _accessRule.HasAccess(apiKey));
    }
    
    public void Dispose()
    {
        _accountService.VerifyAll();
    }

    private Guid SampleGuid()
    {
        return Guid.Parse("5fe7be6c-4ce6-43ce-94f5-e4f91df55a74");
    }
    
    private DeviceApiKey SampleDeviceApiKey()
    {
        return new DeviceApiKey()
        {
            Id = Guid.NewGuid(),
            Name = "Test Key",
            KeyHash = "",
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
