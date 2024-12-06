using CRMSystem.Models;

namespace CRMSystem.ViewModels
{
    public class EventViewModel
    {
        public int ClientId { get; set; }
        public string Type { get; set; }
        public string Result { get; set; }
        public string Description { get; set; }
        public string FollowUpOption { get; set; }
    }
}
