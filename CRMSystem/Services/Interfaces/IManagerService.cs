using System.Collections.Generic;
using System.Threading.Tasks;
using CRMSystem.Models;

namespace CRMSystem.Services.Interfaces
{
    public interface IManagerService
    {
        Task<IEnumerable<User>> GetManagersAsync();
        Task AddManagerAsync(User manager);
        Task DeleteManagerAsync(string id);
        //Task<User> GetManagerByIdAsync(string id);
        //Task UpdateManagerAsync(User manager);
    }
}

