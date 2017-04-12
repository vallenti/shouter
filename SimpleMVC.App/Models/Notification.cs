namespace SimpleMVC.App.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int ShoutId { get; set; }
        public virtual Shout Shout { get; set; }
    }
}
