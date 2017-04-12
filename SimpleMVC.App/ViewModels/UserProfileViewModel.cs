﻿using SimpleMVC.App.Models;
using System.Collections.Generic;

namespace SimpleMVC.App.ViewModels
{
    public class UserProfileViewModel : LoggedUserViewModel
    {
        public IEnumerable<Shout> Shouts { get; set; }
    }
}
