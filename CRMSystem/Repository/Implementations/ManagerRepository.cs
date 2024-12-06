using CRMSystem.DB;
using CRMSystem.Models;
using CRMSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMSystem.Repository.Implementations
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppIdentityContext _context;

        public ManagerRepository(AppIdentityContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetManagersAsync()
        {
            /*var managers = await _context.Users
        .Where(u => !u.IsAdmin && !await _context.UserRoles.AnyAsync(ur => ur.UserId == u.Id && ur.RoleId == Role.Admin)) // Exclude Admins
        .ToListAsync();*/
            var managers = await _context.Users.Where(u => !u.IsAdmin).ToListAsync();
            // Логирование
            Console.WriteLine($"Найдено менеджеров: {managers.Count}");
            return managers;
        }

        public async Task AddManagerAsync(User manager)
        {
            await _context.Users.AddAsync(manager);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteManagerAsync(string id)
        {
            var manager = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (manager != null)
            {
                _context.Users.Remove(manager);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Менеджер с ID {id} не найден.");
            }
        }
    }
}
