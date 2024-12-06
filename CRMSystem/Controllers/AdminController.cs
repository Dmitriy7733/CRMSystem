using CRMSystem.Models;
using CRMSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRMSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IManagerService _managerService;

        public AdminController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var managers = await _managerService.GetManagersAsync();

            return View(managers);
        }

        [HttpPost]
        public async Task<IActionResult> Index(User manager)
        {
            if (ModelState.IsValid)
            {
                manager.IsAdmin = false;

                if (string.IsNullOrEmpty(manager.Id))
                {
                    await _managerService.AddManagerAsync(manager);
                }
                
                return RedirectToAction("Index");
            }
            var managers = await _managerService.GetManagersAsync();
            ViewBag.ManagerToEdit = manager; 
            return View(managers);
        }
        
        [HttpGet]
        public IActionResult AddManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddManager(User manager)
        {
            if (ModelState.IsValid)
            {
                var password = "123"; // Временный пароль
                var passwordHasher = new PasswordHasher<User>();
                manager.PasswordHash = passwordHasher.HashPassword(manager, password);

                manager.Id = Guid.NewGuid().ToString(); 

                await _managerService.AddManagerAsync(manager);

                TempData["TemporaryPasswordMessage"] = $"Менеджер {manager.Name} был успешно добавлен. Временный пароль: {password}";
                return RedirectToAction("Index");
            }
            return View(manager);

        }
       
        [HttpPost] 
        public async Task<IActionResult> Delete(string id)
        {
            await _managerService.DeleteManagerAsync(id);
            return RedirectToAction("Index");
        }
    }
}
