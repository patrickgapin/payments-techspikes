using System.Collections.Generic;

namespace PinnacleSports.RuleService.Models.Notification
{
    public class NotificationModel
    {
        public NotificationModel()
        {
            Message = new List<string>();
        }

        public IList<string> Message { get; set; }
    }
}