using SimpleHttpServer.Models;
using SimpleMVC.App.Models;
using SimpleMVC.App.MVC.Interfaces.Securtiy;
using System.Linq;

namespace SimpleMVC.App.MVC.Security
{
    public class AuthenticationManager
    {
        private IDbIdentityContext dbContext;
        public AuthenticationManager(IDbIdentityContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Register(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }
        public bool SignIn(HttpSession session, string identifyer, string password)
        {
            var user = dbContext.Users
                .FirstOrDefault(u => (u.Email == identifyer || u.Username == identifyer) && u.Password == password);
            if (user == null)
            {
                return false;
            }

            var login = new Login()
            {
                SessionId = session.Id,
                User = user,
                IsActive = true
            };
            dbContext.Logins.Add(login);
            dbContext.SaveChanges();
            return true;
        }

        public bool IsAuthenticated(HttpSession session)
        {
            var sess = dbContext.Logins
                .FirstOrDefault(s => s.SessionId == session.Id && s.IsActive == true);
            if (sess == null)
            {
                return false;
            }
            return true;
        }

        public void SignOut(HttpSession session)
        {
            var sess = dbContext.Logins.FirstOrDefault(s => s.SessionId == session.Id && s.IsActive);
            sess.IsActive = false;
            dbContext.SaveChanges();
        }

        public User GetLoggedUser(HttpSession session)
        {
            var login = dbContext.Logins
               .FirstOrDefault(s => s.SessionId == session.Id && s.IsActive == true);
            if (login == null)
            {
                return null;
            }
            return login.User;
        }

    }
}
