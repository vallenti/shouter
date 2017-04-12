using SimpleMVC.App.Models;
using System.Collections.Generic;

namespace SimpleMVC.App.ViewModels
{
    public class FeedViewModel : LoggedUserViewModel
    {
        public IEnumerable<Shout> Shouts { get; set; }
    }
}
