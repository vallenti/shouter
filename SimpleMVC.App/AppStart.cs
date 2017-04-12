using SimpleHttpServer;
using SimpleMVC.App.MVC;
using SimpleMVC.App.Utilities;

namespace SimpleMVC.App
{
    class AppStart
    {
        static void Main(string[] args)
        {
            ShoutExpirationChecker.Run();
            HttpServer server = new HttpServer(8081, RouteTable.Routes);
            MvcEngine.Run(server);
        }
    }
}
