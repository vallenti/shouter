using SimpleMVC.App.Data;
using SimpleMVC.App.MVC.Controllers;
using SimpleMVC.App.MVC.Security;

namespace SimpleMVC.App.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            this.AuthenticationManager = new AuthenticationManager(new ShouterContext());
        }
        protected AuthenticationManager AuthenticationManager { get; private set; }
    }
}