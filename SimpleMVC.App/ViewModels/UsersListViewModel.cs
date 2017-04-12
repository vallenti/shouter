using SimpleMVC.App.Models;
using System.Collections.Generic;

namespace SimpleMVC.App.ViewModels
{
    public class UsersListViewModel : LoggedUserViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
