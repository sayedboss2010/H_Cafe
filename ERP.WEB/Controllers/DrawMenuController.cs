using ERP.Services.Menu;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WEB.Controllers;

public class DrawMenuController : Controller
{
    private readonly IMenuService _menuService;

    public DrawMenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public IActionResult Index()
    {
        int userId = int.Parse(Request.Cookies.FirstOrDefault(x => x.Key == "UserId").Value);
        var _MenuName = _menuService.DrawMenu(userId);

        return PartialView(_MenuName);
    }
}