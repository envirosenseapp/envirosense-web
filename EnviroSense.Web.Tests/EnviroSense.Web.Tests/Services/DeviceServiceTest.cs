using EnviroSense.Web.Entities;
using EnviroSense.Web.Repositories;
using EnviroSense.Web.Services;
using Moq;

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

    public void Dispose()
    {
        _deviceRepository.VerifyAll();
        _accountService.VerifyAll();
    }
}
