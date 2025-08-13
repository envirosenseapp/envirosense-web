// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Web.Filters;
using EnviroSense.Web.ViewModels.DeviceApiKeys;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.Web.Controllers;

[TypeFilter(typeof(SignedInFilter))]
public class DeviceApiKeysController : Controller
{
    private const string TempDataRevealedKey = "RevealedKey";

    private readonly IDeviceService _deviceService;
    private readonly IDeviceApiKeyService _apiKeyService;

    public DeviceApiKeysController(IDeviceService deviceService, IDeviceApiKeyService apiKeyService)
    {
        _deviceService = deviceService;
        _apiKeyService = apiKeyService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid deviceId)
    {
        var device = await _deviceService.Get(deviceId);
        if (device == null)
        {
            return NotFound();
        }

        return View(new CreateDeviceApiKeyViewModel
        {
            DeviceId = device.Id,
            DeviceName = device.Name,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid deviceId, CreateDeviceApiKeyViewModel input)
    {
        if (!ModelState.IsValid)
        {
            return await Create(deviceId);
        }

        var device = await _deviceService.Get(deviceId);
        if (device == null)
        {
            return NotFound();
        }

        var (apiKey, revealedKey) = await _apiKeyService.CreateAsync(device, input.Name);
        TempData[TempDataRevealedKey] = revealedKey;

        return RedirectToAction("Details", new { id = apiKey.Id });
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
                DeviceId = apiKey.DeviceId,
                DeviceName = apiKey.Device?.Name ?? "",
                Name = apiKey.Name,
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
