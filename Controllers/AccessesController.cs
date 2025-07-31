using EnviroSense.Web.Filters;
using EnviroSense.Web.Services;
using EnviroSense.Web.ViewModels.Accesses;
using Microsoft.AspNetCore.Mvc;


namespace EnviroSense.Web.Controllers;
[TypeFilter(typeof(SignedInFilter))]
public class AccessesController : Controller
{
    private readonly IAccessService _accessService;
    public AccessesController(IAccessService accessService)
    {
        _accessService = accessService;
    }
    public async Task<IActionResult> Index()
    {
        var accessList = await _accessService.List();

        var viewModelList = accessList.Select(a => new AccessesViewModel
        {
            Id = a.Id,
            CreatedAt = a.CreatedAt,
            IpAddress = a.IpAddress,
            Client = a.Client,
            Resource = a.Resource,
            AccountId = a.Account?.Id

        }).ToList();

        return View(viewModelList);
    }

}
