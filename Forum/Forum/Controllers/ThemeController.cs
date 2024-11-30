using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

public class ThemeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}