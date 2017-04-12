using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System.IO;
using System.Text;

namespace SimpleMVC.App.Views.Home
{
    class Notifications : IRenderable<NotificationsListViewModel>
    {
        public NotificationsListViewModel Model
        { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(File.ReadAllText("../../content/nav-logged.html"), Model.CurrentUser.Id, Model.CurrentUser.Username);


            sb.Append("<div class=\"container\">");
            sb.Append("<ul>");
            foreach (var notification in Model.Notifications)
            {
                sb.Append($"<li><a href=\"/followers/profile?id={notification.Shout.AuthorId}\">{notification.Shout.Author.Username}</a> has posted a shout</li>");
            }
            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append(File.ReadAllText("../../content/footer.html"));

            return sb.ToString();
        }
    }
}
