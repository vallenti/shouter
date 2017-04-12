using SimpleMVC.App.Models;
using System.Data.Entity;

namespace SimpleMVC.App.MVC.Interfaces.Securtiy
{
    public interface IDbIdentityContext
    {
        DbSet<User> Users { get; }
        DbSet<Login> Logins { get; }
        void SaveChanges();
    }
}
