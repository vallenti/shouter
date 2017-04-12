using SimpleMVC.App.MVC.Interfaces;
using System.IO;

namespace SimpleMVC.App.Views.Home
{
    public class Login : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText("../../content/login.html");
        }
    }
}
