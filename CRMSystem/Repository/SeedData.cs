using CRMSystem.DB;
using CRMSystem.Models;

namespace CRMSystem.Repository
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (context.Users.Any() || context.Clients.Any() || context.Events.Any())
                {
                    return;
                }

                var adminUser = new User
                {
                    Name = "Admin",
                    Password = "admin_password", 
                    IsAdmin = true
                };

                var managerUser1 = new User
                {
                    Name = "Manager1",
                    Password = "manager_password1", 
                    IsAdmin = false
                };
                var managerUser2 = new User
                {
                    Name = "Manager2",
                    Password = "manager_password2",
                    IsAdmin = false
                };

                context.Users.AddRange(adminUser, managerUser1, managerUser2);
                context.SaveChanges();

                
                var client1 = new Client
                {
                    Name = "Client A",
                    ContactInfo = "contact@clienta.com",
                    Address = "123 Main St",
                    City = "CityA",
                    Region = "RegionA",
                    PostalCode = "12345",
                    Country = "CountryA",
                    Phone = "1234567890",
                    Fax = "1234567891",
                    HomePage = "http://clienta.com",
                    Extension = "001",
                    PhoneNumber = "1234567890"
                };

                var client2 = new Client
                {
                    Name = "Client B",
                    ContactInfo = "contact@clientb.com",
                    Address = "456 Secondary St",
                    City = "CityB",
                    Region = "RegionB",
                    PostalCode = "67890",
                    Country = "CountryB",
                    Phone = "0987654321",
                    Fax = "0987654322",
                    HomePage = "http://clientb.com",
                    Extension = "002",
                    PhoneNumber = "0987654321"
                };
                var client3 = new Client
                {
                    Name = "Client C",
                    ContactInfo = "contact@clienta.com",
                    Address = "ул. Красная, 57",
                    City = "Краснодар",
                    Region = "Краснодарский край",
                    PostalCode = "12345",
                    Country = "Country С",
                    /*Phone = "1234567890",
                    Fax = "1234567891",
                    HomePage = "http://clienta.com",
                    Extension = "001",*/
                    PhoneNumber = "1234567890"
                };

                var client4 = new Client
                {
                    Name = "Client D",
                    ContactInfo = "contact@clientb.com",
                    Address = "Промышленная, 5",
                    City = "Москва",
                    Region = "Region D",
                    PostalCode = "67890",
                    Country = "Россия",
                    Phone = "0987654321",
                    Fax = "0987654322",
                    HomePage = "http://clientb.com",
                    Extension = "002",
                    PhoneNumber = "0987654321"
                };
                var client5 = new Client
                {
                    Name = "Client E",
                    ContactInfo = "contact@clienta.com",
                    Address = "Чкалова, 77",
                    City = "CityA",
                    Region = "Region E",
                    PostalCode = "12345",
                    Country = "Country E",
                    Phone = "1234567890",
                    Fax = "1234567891",
                    HomePage = "http://clienta.com",
                    Extension = "001",
                    PhoneNumber = "1234567890"
                };

                var client6 = new Client
                {
                    Name = "Client I",
                    ContactInfo = "contact@clientb.com",
                    Address = "Пушкина, 145",
                    City = "Питер",
                    Region = "Region I",
                    PostalCode = "67890",
                    Country = "CountryB",
                    Phone = "0987654321",
                    Fax = "0987654322",
                    HomePage = "http://clientb.com",
                    Extension = "002",
                    PhoneNumber = "0987654321"
                };
                var client7 = new Client
                {
                    Name = "Client 7",
                    ContactInfo = "contact@clienta.com",
                    Address = "Федосеева, 777",
                    City = "Орел",
                    Region = "Region7",
                    PostalCode = "12345",
                    Country = "Россия",
                    Phone = "1234567890",
                    Fax = "1234567891",
                    HomePage = "http://clienta.com",
                    Extension = "001",
                    PhoneNumber = "1234567890"
                };

                var client8 = new Client
                {
                    Name = "Client 8",
                    ContactInfo = "contact@clientb.com",
                    Address = "456 Secondary St",
                    City = "City8",
                    Region = "Region8",
                    PostalCode = "67890",
                    Country = "Country8",
                    Phone = "0987654321",
                    Fax = "0987654322",
                    HomePage = "http://clientb.com",
                    Extension = "002",
                    PhoneNumber = "0987654321"
                };
                var client9 = new Client
                {
                    Name = "Client 9",
                    ContactInfo = "contact@clienta.com",
                    Address = "123 Main St",
                    City = "City9",
                    Region = "Region9",
                    PostalCode = "12345",
                    Country = "CountryA",
                    Phone = "1234567890",
                    Fax = "1234567891",
                    HomePage = "http://clienta.com",
                    Extension = "001",
                    PhoneNumber = "1234567890"
                };

                var client10 = new Client
                {
                    Name = "Client 10",
                    ContactInfo = "contact@clientb.com",
                    Address = "456 Secondary St",
                    City = "City10",
                    Region = "Region10",
                    PostalCode = "67890",
                    Country = "Country10",
                    Phone = "0987654321",
                    Fax = "0987654322",
                    HomePage = "http://clientb.com",
                    Extension = "002",
                    PhoneNumber = "0987654321"
                };
                context.Clients.AddRange(client1, client2, client3, client4, client5, client6, client7, client8, client9, client10);
                context.SaveChanges();

                // Добавляем события для клиентов
                var event1 = new Event
                {
                    ClientId = client1.Id,
                    Type = "Встреча",
                    Result = "Успешно",
                    Description = "Обсудили проект, клиент заинтересован.",
                    FollowUpOption = "Готов заключить договор",
                    Client = client1
                };

                var event2 = new Event
                {
                    ClientId = client1.Id,
                    Type = "Договор",
                    Result = "Подписан",
                    Description = "Подписали договор на услуги",
                    FollowUpOption = "Нет дополнительных действий",
                    Client = client1
                };

                var event3 = new Event
                {
                    ClientId = client2.Id,
                    Type = "Звонок",
                    Result = "Неудачно",
                    Description = "Клиент не ответил на звонок.",
                    FollowUpOption = "Перезвонить",
                    Client = client2
                };

                context.Events.AddRange(event1, event2, event3);
                context.SaveChanges();
            }
        }
    }
}
