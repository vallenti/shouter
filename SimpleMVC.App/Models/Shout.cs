using System;

namespace SimpleMVC.App.Models
{
    public class Shout
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime? ExpiresOn { get; set; }
    }
}
