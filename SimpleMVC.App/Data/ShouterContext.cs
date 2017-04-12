namespace SimpleMVC.App.Data
{
    using Models;
    using MVC.Interfaces.Securtiy;
    using System.Data.Entity;

    public class ShouterContext : DbContext, IDbIdentityContext
    {
        public ShouterContext()
            : base("name=ShouterContext")
        {
        }

        public virtual DbSet<Login> Logins { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Shout> Shouts { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        void IDbIdentityContext.SaveChanges()
        {
            this.SaveChanges();
        }


    }
}