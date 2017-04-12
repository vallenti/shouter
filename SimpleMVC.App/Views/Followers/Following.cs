using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System.IO;
using System.Text;

namespace SimpleMVC.App.Views.Followers
{
    public class Following : IRenderable<UsersListViewModel>
    {
        public UsersListViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(File.ReadAllText("../../content/nav-logged.html"), Model.CurrentUser.Id, Model.CurrentUser.Username);
            sb.AppendLine(File.ReadAllText("../../content/search-followers-form.html"));
            sb.Append("<div class=\"container\">");
            sb.Append("<ul>");
            foreach (var user in Model.Users)
            {
                sb.Append($"<li><h4><a href=\"/followers/profile?id={user.Id}\">{user.Username}</a></h4></li>");
            }
            sb.Append("</ul>");

            sb.Append("</div>");
            sb.Append(File.ReadAllText("../../content/footer.html"));
            return sb.ToString();
        }
    }
}
