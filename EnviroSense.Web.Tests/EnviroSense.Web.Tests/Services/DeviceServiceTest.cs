using EnviroSense.Web.Entities;
using EnviroSense.Web.Repositories;
using EnviroSense.Web.Services;
using Moq;
using Moq.Protected;

namespace EnviroSense.Web.Tests.Services;

public class DeviceServiceTest : IDisposable
{
    private readonly Mock<IDeciveRepository> _deviceRepository;
    private readonly Mock<IAccountService> _accountService;
    private readonly DeviceService _deviceService;

    public DeviceServiceTest()
    {
        _deviceRepository = new Mock<IDeciveRepository>();
        _accountService = new Mock<IAccountService>();

        _deviceService = new DeviceService(_deviceRepository.Object, _accountService.Object);
    }

    [Fact]
    public async Task List_It_successfully_fetches_data()
    {
        _accountService.Setup(e => e.GetAccountIdFromSession()).Returns("01a4260a-ef07-47ef-97f8-1ca333fd930a");

        _deviceRepository.Setup(e => e.ListAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new List<Device>()
        {
            new Device
            {
                Name = "test 2",
                Account = new Account() { Devices = new List<Device>(), Email = "123", Password = "123", },
                Measurements = new List<Measurement>(),
            },
            new Device
            {
                Name = "test",
                Account = new Account() { Devices = new List<Device>(), Email = "123", Password = "123", },
                Measurements = new List<Measurement>(),
            },
        }));

        var res = await _deviceService.List();

        Assert.True(res.Count == 2);
    }

    [Fact]
    public async Task List_It_fails_when_account_id_from_session_is_not_found()
    {
        _accountService.Setup(e => e.GetAccountIdFromSession()).Throws(new Exception("Session is available"));

        await Assert.ThrowsAsync<Exception>(async () => await _deviceService.List());
    }

    [Fact]
    public async Task Get_device_if_id_is_found()
    {
        _deviceRepository.Setup(e => e.GetAsync(It.IsAny<Guid>())).ReturnsAsync((new Device
        {
            Name = "test 2",
            Account = new Account() { Devices = new List<Device>(), Email = "123", Password = "123", },
            Measurements = new List<Measurement>(),
        }));

        var res = await _deviceService.Get(Guid.NewGuid());
        Assert.NotNull(res);
        Assert.True(res.Name == "test 2");
    }


    [Fact]
    public async Task Get_device_if_id_is_not_found_should_return_null()
    {
        _deviceRepository
            .Setup(a => a.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Device?)null);

        var result = await _deviceService.Get(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task Create_device()
    {
        var testId = Guid.NewGuid();
        var mockDeviceService = new Mock<DeviceService>(
            _deviceRepository.Object, _accountService.Object
        )
        { CallBase = true };
        mockDeviceService.Protected().Setup<Guid>("GetAccountId").Returns(testId);

        _accountService.Setup(a => a.GetAccountById(testId)).ReturnsAsync(new Account
        {
            Id = testId,
            Email = "123",
            Password = "123",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Devices = new List<Device>(),
            Accesses = new List<Access>(),
        });

        _deviceRepository.Setup(d => d.CreateAsync(It.IsAny<Device>())).ReturnsAsync((Device d) => d);

        var savedDevice = await mockDeviceService.Object.Create("Thermometer");

        Assert.Equal("Thermometer", savedDevice.Name);
    }

    public void Dispose()
    {
        _deviceRepository.VerifyAll();
        _accountService.VerifyAll();
    }
}
