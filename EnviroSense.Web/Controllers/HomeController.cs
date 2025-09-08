using System.Diagnostics;
using EnviroSense.Application.Services;
using EnviroSense.Web.Filters;
using EnviroSense.Web.ViewModels;
using EnviroSense.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.Web.Controllers;

[TypeFilter(typeof(SignedInFilter))]
public class HomeController : Controller
{
    private readonly IDeviceService _deviceService;
    private readonly IMeasurementService _measurementService;

    public HomeController(IDeviceService deviceService, IMeasurementService measurementService)
    {
        _deviceService = deviceService;
        _measurementService = measurementService;
    }

    public async Task<IActionResult> Index()
    {
        var deviceList = await _deviceService.List(0);
        var allMeasurements = new List<DeviceMeasurementListViewModel>();

        foreach (var device in deviceList.Records)
        {
            var measurement = await _measurementService.GetLastest(device.Id);

            if (measurement == null)
            {
                allMeasurements.Add(new DeviceMeasurementListViewModel()
                {
                    DeviceName = device.Name,
                    Temperature = null,
                    Humidity = null,
                    RecordingDate = null
                });
            }
            else
            {
                allMeasurements.Add(new DeviceMeasurementListViewModel()
                {
                    DeviceId = device.Id,
                    DeviceName = device.Name,
                    Temperature = measurement.Temperature,
                    Humidity = measurement.Humidity,
                    RecordingDate = measurement.RecordingDate
                });
            }
        }
        var model = new DeviceMeasurementViewModel
        {
            TotalPages = deviceList.TotalPages,
            DeviceMeasurementList = allMeasurements
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
