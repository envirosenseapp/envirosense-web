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
        var accountId = await _sessionAuthentication.CurrentAccountId();
        var list = await _apiKeyService.List(accountId.Value);
        var viewModel = list.Select(a => new ApiKeysViewModel
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
    public async Task<IActionResult> Create(CreateDeviceApiKeyViewModel input)
    {
        if (!ModelState.IsValid)
        {
            return Create();
        }

        try
        {
            var accountId = await _sessionAuthentication.CurrentAccountId();
            var account = await _accountService.GetAccountById(accountId.Value);

            var (apiKey, revealedKey) = await _apiKeyService.CreateAsync(input.Name, account);
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
            var viewModel = new DetailsDeviceApiKeyViewModel
            {
                Id = apiKey.Id,
                KeyName = apiKey.Name,
                Owner = apiKey.Account.Email,
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
