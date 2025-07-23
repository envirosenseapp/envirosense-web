using EnviroSense.Web.Services;
using Microsoft.AspNetCore.Mvc;
using EnviroSense.Web.ViewModels.Devices;

namespace EnviroSense.Web.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceService _deviceService;
        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
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
        public async Task<ActionResult> Add(string name)
        {
            if (name == null)
            {
                return View();
            }
            var newDevice = await _deviceService.Create(name);

            return RedirectToAction("Details", new { Id = newDevice.Id });
        }
    }
}
