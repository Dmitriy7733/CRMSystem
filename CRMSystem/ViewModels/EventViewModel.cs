using CRMSystem.Models;

namespace CRMSystem.ViewModels
{
    public class EventViewModel
    {
        /*public int Id { get; set; }
        public int ClientId { get; set; }
        public string Type { get; set; } // "Встреча", "Звонок", "Договор"
        public string? Result { get; set; }
        public string? Description { get; set; }
        public string FollowUpOption { get; set; } // Перезвонить, Готов заключить договор и т.д.
        public Client Client { get; set; }
        public IEnumerable<Event>? Events { get; set; }*/
        public int ClientId { get; set; }
        public string Type { get; set; }
        public string Result { get; set; }
        public string Description { get; set; }
        public string FollowUpOption { get; set; }
    }
}
