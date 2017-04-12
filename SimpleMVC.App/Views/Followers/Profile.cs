using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.Utilities;
using SimpleMVC.App.ViewModels;
using System.IO;
using System.Text;

namespace SimpleMVC.App.Views.Followers
{
    public class Profile : IRenderable<UserProfileViewModel>
    {
        public UserProfileViewModel Model { get; set; }
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(File.ReadAllText("../../content/nav-logged.html"), Model.CurrentUser.Id, Model.CurrentUser.Username);
            sb.Append("<div class=\"container\">");
            sb.AppendFormat("<h2>{0}</h2>", Model.CurrentUser.Username);

            foreach (var shout in Model.Shouts)
            {
                sb.AppendFormat(File.ReadAllText("../../content/shout-profile.html"), shout.Author.Username, shout.PublishedOn.ToRelativeTime(), shout.Content, shout.Id, shout.AuthorId);
            }
            sb.Append("</div>");
            sb.Append(File.ReadAllText("../../content/footer.html"));
            return sb.ToString();
        }
    }
}
