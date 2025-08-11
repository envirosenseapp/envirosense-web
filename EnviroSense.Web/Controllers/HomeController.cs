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
        var deviceList = await _deviceService.List();
        var allMeasurements = new List<DeviceMeasurmentViewModel>();

        foreach (var device in deviceList)
        {
            var measurement = await _measurementService.GetLastest(device.Id);

            if (measurement == null)
            {
                allMeasurements.Add(new DeviceMeasurmentViewModel
                {
                    DeviceName = device.Name,
                    Temperature = null,
                    Humidity = null,
                    RecordingDate = null
                });
            }
            else
            {
                allMeasurements.Add(new DeviceMeasurmentViewModel
                {
                    DeviceName = device.Name,
                    Temperature = measurement.Temperature,
                    Humidity = measurement.Humidity,
                    RecordingDate = measurement.RecordingDate
                });
            }
        }

        return View(allMeasurements);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
