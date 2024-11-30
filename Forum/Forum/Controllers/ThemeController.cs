using Forum.Models;
using Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers;

public class ThemeController : Controller
{
    public readonly ForumContext _context;
    public readonly UserManager<User> _UserManager;

    public ThemeController(ForumContext context, UserManager<User> userManager)
    {
        _context = context;
        _UserManager = userManager;
    }
    
    public async Task<IActionResult> Index(int page = 1)
    {
        int pageSize = 3;
        var themes = await _context.Themes.Include(t => t.User).Include(t => t.Messages).OrderByDescending(t => t.Description).ToListAsync();
        var count = themes.Count();
        var items = themes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        ThemeIndexViewModel viewModel = new ThemeIndexViewModel()
        {
            PageViewModel = pageViewModel,
            Themes = items
        };
        return View(viewModel);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Theme theme)
    {
        int? userId = Convert.ToInt32(_UserManager.GetUserId(User));
        theme.UserId = userId.Value;
        theme.DateOfCreation = DateTime.UtcNow;
        if (ModelState.IsValid)
        {
            await _context.AddAsync(theme);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        return View(theme);
    }

    public async Task<IActionResult> Details()
    {
        return View();
    }
}