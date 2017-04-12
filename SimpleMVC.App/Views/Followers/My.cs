using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.Utilities;
using SimpleMVC.App.ViewModels;
using System.IO;
using System.Text;

namespace SimpleMVC.App.Views.Followers
{
    class My : IRenderable<FeedViewModel>
    {
        public FeedViewModel Model { get; set; }
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            var html = File.ReadAllText("../../content/nav-logged.html");
            sb.Append(string.Format(html, Model.CurrentUser.Id, Model.CurrentUser.Username));

            sb.Append("<div class=\"container\">");
            foreach (var shout in Model.Shouts)
            {
                sb.AppendFormat(File.ReadAllText("../../content/shout.html"), shout.AuthorId, shout.Author.Username, shout.PublishedOn.ToRelativeTime(), shout.Content.ApplyHyperlinks());
            }
            sb.Append("</div>");
            sb.Append(File.ReadAllText("../../content/footer.html"));

            return sb.ToString();
        }
    }
}
