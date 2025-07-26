using System.Diagnostics;
using EnviroSense.Web.Models;
using EnviroSense.Web.Services;
using EnviroSense.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace EnviroSense.Web.Controllers;

public class HomeController : Controller
{
    private readonly IAccessService _accessService;
    public HomeController(IAccessService accessService)
    {
        _accessService = accessService;
    }
    public async Task<IActionResult> Index()
    {
        var accessCount = await _accessService.Count();

        return View(new IndexViewModel
        {
            TotalAccesses = accessCount,
        });
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
