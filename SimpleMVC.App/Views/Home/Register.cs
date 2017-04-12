using SimpleMVC.App.MVC.Interfaces;
using System.IO;

namespace SimpleMVC.App.Views.Home
{
    public class Register : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText("../../content/register.html");
        }
    }
}
