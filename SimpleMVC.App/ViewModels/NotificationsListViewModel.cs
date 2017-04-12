using SimpleMVC.App.Models;
using System.Collections.Generic;

namespace SimpleMVC.App.ViewModels
{
    public class NotificationsListViewModel : LoggedUserViewModel
    {
        public IEnumerable<Notification> Notifications { get; set; }
    }
}
