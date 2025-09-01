using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Web.Authentication;
using EnviroSense.Web.Filters;
using EnviroSense.Web.ViewModels.ApiKeys;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.Web.Controllers;

[TypeFilter(typeof(SignedInFilter))]
public class ApiKeysController : Controller
{
    private const string TempDataRevealedKey = "RevealedKey";

    private readonly IDeviceService _deviceService;
    private readonly IApiKeyService _apiKeyService;
    private readonly ISessionAuthentication _sessionAuthentication;
    private readonly IAccountService _accountService;

    public ApiKeysController(IDeviceService deviceService, IApiKeyService apiKeyService, ISessionAuthentication sessionAuthentication, IAccountService accountService)
    {
        _deviceService = deviceService;
        _apiKeyService = apiKeyService;
        _sessionAuthentication = sessionAuthentication;
        _accountService = accountService;
    }

    public async Task<IActionResult> ApiKeys()
    {
        var list = await _apiKeyService.List();
        var viewModel = list.Select(a => new ApiKeyViewModel
        {
            Id = a.Id,
            Name = a.Name,
            DisabledAt = a.DisabledAt,
        }).ToList();
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateApiKeyViewModel input)
    {
        if (!ModelState.IsValid)
        {
            return Create();
        }

        try
        {
            var (apiKey, revealedKey) = await _apiKeyService.CreateAsync(input.Name);
            TempData[TempDataRevealedKey] = revealedKey;

            return RedirectToAction("Details", new { id = apiKey.Id });
        }
        catch (DeviceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var apiKey = await _apiKeyService.GetByIdAsync(id);
            var viewModel = new DetailsApiKeyViewModel
            {
                Id = apiKey.Id,
                KeyName = apiKey.Name,
                RevealedKey = TempData[TempDataRevealedKey]?.ToString(),
                DisabledAt = apiKey.DisabledAt,
                CreatedAt = apiKey.CreatedAt,
            };

            return View(viewModel);
        }
        catch (DeviceApiKeyNotFound)
        {
            return NotFound();
        }
    }
}
