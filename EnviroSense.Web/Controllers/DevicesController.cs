using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Web.Filters;
using EnviroSense.Web.ViewModels.Devices;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.Web.Controllers
{
    [TypeFilter(typeof(SignedInFilter))]
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

        public async Task<ActionResult> Details(Guid iD)
        {
            var deviceDetails = await _deviceService.Get(iD);

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
        public async Task<IActionResult> AddMeasurements(MeasurementViewModel model)
        {
            if (!model.Temperature.HasValue || !model.Humidity.HasValue)
            {
                return RedirectToAction("AddMeasurements", new { deviceId = model.DeviceId });
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddMeasurements", new { deviceId = model.DeviceId });
            }

            try

            {
                ViewBag.DeviceId = model.DeviceId;

                model.RecordingDate = model.RecordingDate.ToUniversalTime();
                var device = await _measurementService.Get(model.DeviceId);
                var measurementModel = new Measurement()
                {
                    DeviceId = model.DeviceId,
                    Temperature = model.Temperature.Value,
                    Humidity = model.Humidity.Value,
                    RecordingDate = model.RecordingDate,
                    Device = device
                };

                var newMeasurement = await _measurementService.Create(measurementModel);

                return RedirectToAction("Measurements", new { deviceId = newMeasurement.DeviceId });
            }

            catch
                (DeviceNotFoundException except)
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
