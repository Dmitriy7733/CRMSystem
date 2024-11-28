using Microsoft.AspNetCore.Authorization;
using CRMSystem.DB;
using CRMSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CRMSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

//[Authorize(Roles = Role.Admin)]
public class AdminController : Controller
{
    private readonly AppIdentityContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(AppIdentityContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        // Получаем список всех пользователей
        var users = await _userManager.Users.ToListAsync();

        // Создаем список менеджеров
        var managers = new List<IdentityUser>();

        // Проверяем каждого пользователя на принадлежность к роли "Manager"
        foreach (var user in users)
        {
            if (await _userManager.IsInRoleAsync(user, Role.Manager))
            {
                managers.Add(user);
            }
        }

        // Создаем экземпляр ViewModel и передаем в представление
        var viewModel = new ManagerListViewModel
        {
            Users = managers.Select(m => new User { Name = m.UserName }).ToList() // Преобразуем в нужный формат
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddManager(string name, string password)
    {
        // Проверяем, что имя пользователя уникально
        var existingUser = await _userManager.FindByNameAsync(name);
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Пользователь с таким именем уже существует.");
            return View("Index", await GetManagerListViewModel());
        }

        var user = new IdentityUser { UserName = name };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Role.Manager);
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View("Index", await GetManagerListViewModel());
    }

    private async Task<ManagerListViewModel> GetManagerListViewModel()
    {
        var users = await _userManager.Users.ToListAsync();
        var managers = new List<IdentityUser>();

        foreach (var user in users)
        {
            if (await _userManager.IsInRoleAsync(user, Role.Manager))
            {
                managers.Add(user);
            }
        }

        return new ManagerListViewModel
        {
            Users = managers.Select(m => new User { Name = m.UserName }).ToList()
        };
    }
}