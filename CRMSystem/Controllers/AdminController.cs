using CRMSystem.DB;
using CRMSystem.Models;
using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        if (User.IsInRole(Role.Admin))
        {
            user.IsAdmin = false; 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return Unauthorized(); 
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        
        if (User.IsInRole("Admin")) 
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null && !user.IsAdmin) 
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        return Unauthorized(); 
    }
}