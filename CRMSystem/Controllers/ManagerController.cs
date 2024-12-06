
using CRMSystem.Models;
using CRMSystem.Repository.Interfaces;
using CRMSystem.Services;
using CRMSystem.Services.Interfaces;
using CRMSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;



namespace CRMSystem.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IClientService _clientService;

        public ManagerController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public ViewResult Index(int page = 1)
        {
            var result = _clientService.GetClientByPage(page);
            return View(result);
        }

        public ViewResult Edit(int id)
        {
            var client = _clientService.GetClient(id);
            var viewModel = new Client
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
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientService.SaveClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public IActionResult Delete(int id)
        {
            _clientService.DeleteClient(id);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ViewResult AddClient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddClient(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientService.AddClient(client);
                return RedirectToAction("Index");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(client);
        }

        [HttpGet]
        public IActionResult AddEvent(int clientId)
        {
            var eventViewModel = new EventViewModel
            {
                ClientId = clientId
            };
            return View(eventViewModel);
        }

        [HttpPost]
        public IActionResult AddEvent(EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                
                {
                    _clientService.AddEvent(eventViewModel);
                    return RedirectToAction("Events", new { clientId = eventViewModel.ClientId });
                }

            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(eventViewModel);
        }

        public IActionResult Events(int clientId)
        {
            var events = _clientService.GetEventsByClientId(clientId);

            var eventViewModels = events.Select(e => new EventViewModel
            {
                ClientId = e.ClientId,
                Type = e.Type,
                Result = e.Result,
                Description = e.Description,
                FollowUpOption = e.FollowUpOption
            }).ToList(); 

            return View(eventViewModels);
        }

    }
}
