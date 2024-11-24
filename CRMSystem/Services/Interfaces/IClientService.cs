using CRMSystem.Models;
using CRMSystem.ViewModels;

namespace CRMSystem.Services.Interfaces
{
    public interface IClientService
    {
        Client GetClient(int id);
        ClientListViewModel GetClientByPage(int page);
        void SaveClient(Client client);
        void DeleteClient(int id);
        void AddEvent(EventViewModel eventViewModel);
        IEnumerable<Event> GetEventsByClientId(int clientId);
        void AddClient(Client client);
    }
}


