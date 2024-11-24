using CRMSystem.DB;
using CRMSystem.Models;
using CRMSystem.Repository.Interfaces;
using CRMSystem.ViewModels;


namespace CRMSystem.Repository.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;

            Clients = _context.Clients;
        }


        public IEnumerable<Client> Clients { get; }

        public void Save(Client client)
        {
            if (client.Id == 0)
            {
                // Новый клиент
                _context.Clients.Add(client);
            }
            else
            {
                // Существующий клиент
                var dbEntry = _context.Clients.FirstOrDefault(e => e.Id == client.Id);
                if (dbEntry != null)
                {
                    // Обновляем поля
                    dbEntry.Name = client.Name;
                    dbEntry.ContactInfo = client.ContactInfo;
                    dbEntry.Address = client.Address;
                    dbEntry.City = client.City;
                    dbEntry.Region = client.Region;
                    dbEntry.PostalCode = client.PostalCode;
                    dbEntry.Country = client.Country;
                    dbEntry.Phone = client.Phone;
                    dbEntry.Fax = client.Fax;
                    dbEntry.HomePage = client.HomePage;
                    dbEntry.Extension = client.Extension;
                    dbEntry.PhoneNumber = client.PhoneNumber;
                }
            }
            _context.SaveChanges();
        }


        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var client = _context.Clients.FirstOrDefault(e => e.Id == id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }

        public Client GetClientById(long id)
        {
            return _context.Clients.FirstOrDefault(e => e.Id == id);
        }


        public void AddEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
        }

        public IEnumerable<Event> GetEventsByClientId(long clientId)
        {
            return _context.Events.Where(e => e.ClientId == clientId).ToList();
        }
    }
}
