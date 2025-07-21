using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EnviroSense.Web.Models;
using EnviroSense.Web.Services;
using EnviroSense.Web.ViewModels.Home;

namespace EnviroSense.Web.Controllers;

public class HomeController : Controller
{
    private readonly IAccessService _accessService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {

        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {

        var accessCount = Convert.ToInt32(HttpContext.Items["totalAccess"]);

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
