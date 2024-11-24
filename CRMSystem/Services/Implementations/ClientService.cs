using CRMSystem.Repository.Interfaces;
using CRMSystem.Services.Interfaces;
using CRMSystem.Models;
using CRMSystem.ViewModels;
using Microsoft.EntityFrameworkCore;




namespace CRMSystem.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly int _pageSize = 4;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public Client GetClient(int id)
        {
            return _repository.GetClientById(id);
        }

        public ClientListViewModel GetClientByPage(int page)
        {
            
            var totalItems = _repository.Clients.Count();

            
            var clients = _repository.Clients
                .OrderBy(e => e.Id)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = _pageSize,
                TotalItems = totalItems
            };

            var result = new ClientListViewModel
            {
                Clients = clients,
                PagingInfo = pagingInfo
            };

            return result;
        }

        public void SaveClient(Client client)
        {
            var clientEntity = new Client
            {
                Id = client.Id,
                Name = client.Name,
                ContactInfo = client.ContactInfo,
                Address = client.Address,
                City = client.City,
                Region = client.Region,
                PostalCode = client.PostalCode,
                Country = client.Country,
                Phone = client.Phone,
                Fax = client.Fax,
                HomePage = client.HomePage,
                Extension = client.Extension,
                PhoneNumber = client.PhoneNumber
            };

            _repository.Save(clientEntity);
        }

        public void DeleteClient(int id)
        {
            _repository.Delete(id);
        }
        public IEnumerable<Event> GetEventsByClientId(int clientId)
        {
            return _repository.GetEventsByClientId(clientId);
        }
        public void AddEvent(EventViewModel eventViewModel)
        {
            try
            {
                var clientExists = _repository.Clients.Any(c => c.Id == eventViewModel.ClientId);
                if (!clientExists)
                {
                    throw new Exception($"Клиент с ID {eventViewModel.ClientId} не существует.");
                }

                var newEvent = new Event
                {
                    ClientId = eventViewModel.ClientId,
                    Type = eventViewModel.Type,
                    Result = eventViewModel.Result,
                    Description = eventViewModel.Description,
                    FollowUpOption = eventViewModel.FollowUpOption
                };

                _repository.AddEvent(newEvent);
            }
            catch (Exception ex)
            {
                // Логируйте или обрабатывайте исключение
                throw new Exception("Ошибка при добавлении события: " + ex.Message);
            }
        }


        public void AddClient(Client client)
        {
            var clientEntity = new Client
            {
                Id = client.Id,
                Name = client.Name,
                ContactInfo = client.ContactInfo,
                Address = client.Address,
                City = client.City,
                Region = client.Region,
                PostalCode = client.PostalCode,
                Country = client.Country,
                Phone = client.Phone,
                Fax = client.Fax,
                HomePage = client.HomePage,
                Extension = client.Extension,
                PhoneNumber = client.PhoneNumber
            };
            _repository.AddClient(clientEntity);

            var newEvent = new Event
            {
                ClientId = clientEntity.Id, 
                Type = "Создание клиента", 
                Result = "Успешно", 
                Description = $"Клиент {clientEntity.Name} был добавлен.", 
                FollowUpOption = "Нет дополнительных действий" 
            };
            _repository.AddEvent(newEvent);
        }
    }

}


