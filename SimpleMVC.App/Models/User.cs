using System;
using System.Collections.Generic;

namespace SimpleMVC.App.Models
{
    public class User
    {
        public User()
        {
            this.Logins = new HashSet<Login>();
            this.Shouts = new HashSet<Shout>();
            this.Followers = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Shout> Shouts { get; set; }
        public virtual ICollection<User> Followers { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as User;
            if (other == null)
            {
                return false;
            }
            if (this.Id != other.Id)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
