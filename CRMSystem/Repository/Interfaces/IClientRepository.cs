using CRMSystem.Models;

namespace CRMSystem.Repository.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> Clients { get; }
        void Save(Client client);
        void Delete(long id);
        Client GetClientById(long id);
        void AddEvent(Event newEvent);
        IEnumerable<Event> GetEventsByClientId(long clientId);
         void AddClient(Client client);

    }
}
