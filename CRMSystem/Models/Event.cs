namespace CRMSystem.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string? Type { get; set; } // "Встреча", "Звонок", "Договор"
        public string? Result { get; set; }
        public string? Description { get; set; }
        public string? FollowUpOption { get; set; } // Перезвонить, Готов заключить договор
        public Client Client { get; set; }
        public IEnumerable<Event>? Events { get; set; }
    }
}
