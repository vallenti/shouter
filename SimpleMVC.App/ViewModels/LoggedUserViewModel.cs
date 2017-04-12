using SimpleMVC.App.Models;

namespace SimpleMVC.App.ViewModels
{
    public class LoggedUserViewModel
    {
        public bool IsLoggedIn { get; set; }
        public User CurrentUser { get; set; }
    }
}
