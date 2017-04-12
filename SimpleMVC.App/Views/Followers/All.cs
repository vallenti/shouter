using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System.IO;
using System.Text;

namespace SimpleMVC.App.Views.Followers
{
    public class All : IRenderable<UsersListViewModel>
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
                sb.Append("<li>");
                sb.Append($"<h4><a href=\"/followers/profile?id={user.Id}\">{user.Username}</a></h4>");
                if (Model.CurrentUser.Followers.Contains(user))
                {
                    sb.Append("<form method=\"POST\" action=\"/followers/unfollow\">");
                    sb.Append($"<input type=\"hidden\" name=\"CurrentUserId\" value=\"{Model.CurrentUser.Id}\" /> ");
                    sb.Append($"<input type=\"hidden\" name=\"FollowerId\" value=\"{user.Id}\" /> ");
                    sb.Append("<input class=\"btn btn-danger\" type=\"submit\" value=\"Unfollow\"/>");
                    sb.Append("</form>");
                }
                else
                {
                    sb.Append("<form method=\"POST\" action=\"/followers/follow\">");
                    sb.Append($"<input type=\"hidden\" name=\"CurrentUserId\" value=\"{Model.CurrentUser.Id}\" /> ");
                    sb.Append($"<input type=\"hidden\" name=\"FollowerId\" value=\"{user.Id}\" /> ");
                    sb.Append("<input class=\"btn btn-success\" type=\"submit\" value=\"Follow\"/>");
                    sb.Append("</form>");
                }
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            sb.Append("</div>");
            sb.Append(File.ReadAllText("../../content/footer.html"));
            return sb.ToString();
        }
    }
}
