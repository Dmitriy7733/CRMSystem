using CRMSystem.Models;
using CRMSystem.Repository.Interfaces;
using CRMSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMSystem.Services.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _repository;

        public ManagerService(IManagerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetManagersAsync()
        {
            return await _repository.GetManagersAsync();
        }

        public async Task AddManagerAsync(User manager)
        {
            await _repository.AddManagerAsync(manager);
        }

        public async Task DeleteManagerAsync(string id)
        {
            await _repository.DeleteManagerAsync(id);
        }

        /*public async Task<User> GetManagerByIdAsync(string id)
        {
            return await _repository.GetManagerByIdAsync(id);
        }
        public async Task UpdateManagerAsync(User manager)
        {
            await _repository.UpdateManagerAsync(manager);
        }*/
    }
}