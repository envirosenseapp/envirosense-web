using EnviroSense.Application.Authentication;
using EnviroSense.Application.Authorization;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Repositories.Repositories;
using Moq;

namespace EnviroSense.Application.Tests.Services;

public class DeviceServiceTest : IDisposable
{
    private readonly Mock<IDeviceRepository> _deviceRepository;
    private readonly Mock<IAuthenticationContext> _authenticationContext;
    private readonly Mock<IAuthorizationResolver> _authorizationResolver;
    private readonly DeviceService _deviceService;

    public DeviceServiceTest()
    {
        _deviceRepository = new Mock<IDeviceRepository>();
        _authenticationContext = new Mock<IAuthenticationContext>();
        _authorizationResolver = new Mock<IAuthorizationResolver>();

        _deviceService = new DeviceService(_deviceRepository.Object, _authorizationResolver.Object, _authenticationContext.Object);
    }

    [Fact]
    public async Task List_It_successfully_fetches_data()
    {
        _authenticationContext.Setup(e => e.CurrentAccountId()).ReturnsAsync(Guid.Parse("01a4260a-ef07-47ef-97f8-1ca333fd930a"));

        _deviceRepository.Setup(e => e.ListAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new List<Device>()
        {
            new Device
            {
                Name = "test 2",
                Account = new Account() {Email = "123", Password = "123", },
            },
            new Device
            {
                Name = "test",
                Account = new Account() { Email = "123", Password = "123", },
            },
        }));

        var res = await _deviceService.List();

        Assert.True(res.Count == 2);
    }

    [Fact]
    public async Task List_It_fails_when_account_id_from_session_is_not_found()
    {
        _authenticationContext.Setup(e => e.CurrentAccountId()).Throws(new Exception("Session is available"));

        await Assert.ThrowsAsync<Exception>(async () => await _deviceService.List());
    }

    [Fact]
    public async Task Get_device_if_id_is_found()
    {
        var sampleGUID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
        _deviceRepository.Setup(e => e.GetAsync(sampleGUID)).ReturnsAsync((new Device
        {
            Name = "test 2",
            Account = new Account() { Email = "123", Password = "123", },
        }));

        var res = await _deviceService.Get(sampleGUID);
        Assert.NotNull(res);
        Assert.True(res.Name == "test 2");
    }


    [Fact]
    public async Task Get_device_if_id_is_not_found_should_return_null()
    {
        var sampleGUID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
        _deviceRepository
            .Setup(a => a.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Device?)null);

        var result = await _deviceService.Get(sampleGUID);

        Assert.Null(result);
    }

    [Fact]
    public async Task Create_device()
    {
        var testId = Guid.NewGuid();

        _authenticationContext.Setup(a => a.CurrentAccount()).ReturnsAsync(new Account
        {
            Id = testId,
            Email = "123",
            Password = "123",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        });
        _deviceRepository.Setup(d => d.CreateAsync(It.IsAny<Device>())).ReturnsAsync((Device d) => d);

        var savedDevice = await _deviceService.Create("Thermometer");

        Assert.Equal("Thermometer", savedDevice.Name);
    }


    public void Dispose()
    {
        _deviceRepository.VerifyAll();
        _authenticationContext.VerifyAll();
        _authorizationResolver.VerifyAll();
    }
}
