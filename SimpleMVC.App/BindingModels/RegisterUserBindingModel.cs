using System;

namespace SimpleMVC.App.BindingModels
{
    public class RegisterUserBindingModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
