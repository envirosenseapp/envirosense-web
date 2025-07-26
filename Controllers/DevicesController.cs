using EnviroSense.Web.Exceptions;
using EnviroSense.Web.Services;
using EnviroSense.Web.ViewModels.Devices;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.Web.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IMeasurementService _measurementService;

        public DevicesController(IDeviceService deviceService, IMeasurementService measurementService)
        {
            _deviceService = deviceService;
            _measurementService = measurementService;
        }

        public async Task<ActionResult> Index()
        {
            var deviceList = await _deviceService.List();

            var viewModelList = deviceList.Select(d => new DeviceViewModel
            {
                Id = d.Id,
                Name = d.Name,
                UpdatedAt = d.UpdatedAt,
                CreatedAt = d.CreatedAt
            }).ToList();

            return View(viewModelList);
        }

        public async Task<ActionResult> Details(Guid Id)
        {
            var deviceDetails = await _deviceService.Get(Id);

            if (deviceDetails == null)
            {
                return NotFound();
            }

            var viewModeDetails = new DeviceViewModel
            {
                Id = deviceDetails.Id,
                Name = deviceDetails.Name,
                UpdatedAt = deviceDetails.UpdatedAt,
                CreatedAt = deviceDetails.CreatedAt
            };

            return View(viewModeDetails);
        }

        public async Task<ActionResult> Add(string? name)
        {
            if (name == null)
            {
                return View();
            }

            var newDevice = await _deviceService.Create(name);

            return RedirectToAction("Details", new { newDevice.Id });
        }

        [HttpGet]
        public IActionResult AddMeasurements(Guid deviceId)
        {
            ViewBag.DeviceId = deviceId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMeasurements(Guid deviceId, float temperature, float humidity,
            DateTime recordingDate)
        {
            try
            {
                ViewBag.DeviceId = deviceId;

                recordingDate = recordingDate.ToUniversalTime();

                var newMeasurement = await _measurementService.Create(recordingDate, temperature, humidity, deviceId);

                return RedirectToAction("Measurements", new { deviceId = newMeasurement.DeviceId });
            }

            catch (DeviceNotFoundException except)
            {
                return NotFound(new { message = except.Message });
            }
        }

        public async Task<ActionResult> Measurements(Guid deviceId)
        {
            var measurementList = await _measurementService.List(deviceId);

            if (measurementList == null)
            {
                return NotFound();
            }

            var viewModelList = measurementList.Select(m => new MeasurementViewModel
            {
                Id = m.Id,
                DeviceId = m.DeviceId,
                Temperature = m.Temperature,
                Humidity = m.Humidity,
                RecordingDate = m.RecordingDate
            }).ToList();

            return View(viewModelList);
        }
    }
}
