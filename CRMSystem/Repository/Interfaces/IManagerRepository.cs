using System.Collections.Generic;
using System.Threading.Tasks;
using CRMSystem.Models;

namespace CRMSystem.Repository.Interfaces
{
    public interface IManagerRepository
    {
        Task<IEnumerable<User>> GetManagersAsync();
        Task AddManagerAsync(User manager);
        Task DeleteManagerAsync(string id);
    }
}
