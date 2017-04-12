using SimpleHttpServer.Models;
using SimpleMVC.App.BindingModels;
using SimpleMVC.App.Data;
using SimpleMVC.App.Models;
using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleMVC.App.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult<FeedViewModel> Feed(HttpSession session)
        {

            var isLoggedIn = AuthenticationManager.IsAuthenticated(session);
            var shouts = new ShouterContext().Shouts
                .ToList()
                .OrderByDescending(x => x.PublishedOn);
            if (isLoggedIn)
            {
                var user = AuthenticationManager.GetLoggedUser(session);
                var viewModel = new FeedViewModel()
                {
                    IsLoggedIn = isLoggedIn,
                    CurrentUser = user,
                    Shouts = shouts
                };
                return View(viewModel);
            }
            else
            {
                var viewModel = new FeedViewModel()
                {
                    IsLoggedIn = isLoggedIn,
                    Shouts = shouts
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<FeedViewModel> Feed(HttpSession session, ShoutBindingModel model)
        {
            if (model.Content.Length > 160)
            {
                model.Content = model.Content.Substring(0, 160);
            }
            var now = DateTime.Now;
            var expirationDate = model.Expiration == default(int) ? default(DateTime?) : now.AddHours(model.Expiration);
            var shout = new Shout()
            {
                AuthorId = AuthenticationManager.GetLoggedUser(session).Id,
                Content = model.Content,
                PublishedOn = now,
                ExpiresOn = expirationDate
            };
            using (var context = new ShouterContext())
            {
                context.Shouts.Add(shout);
                context.Notifications.Add(new Notification()
                {
                    ShoutId = shout.Id
                });
                context.SaveChanges();
            }
            return Feed(session);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model, HttpResponse response)
        {
            if (Regex.Match(model.Username, "[a-zA-Z0-9]+").Success &&
                Regex.Match(model.Email, "(.+)@(.+)").Success &&
                model.Password.Length > 2 && model.Password == model.ConfirmPassword &&
                DateTime.Now.Year - model.Birthdate.Year > 12)
            {
                var user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Birthdate = model.Birthdate
                };
                AuthenticationManager.Register(user);
                Redirect(response, "/home/login");
                return null;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel model, HttpSession session, HttpResponse response)
        {
            var success = AuthenticationManager.SignIn(session, model.Identifyer, model.Password);
            if (success)
            {
                Redirect(response, "/home/feed");
                return null;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout(HttpSession session, HttpResponse response)
        {
            AuthenticationManager.SignOut(session);
            Redirect(response, "/home/feed");
            return null;
        }

        [HttpGet]
        public IActionResult<NotificationsListViewModel> Notifications(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session))
            {
                Redirect(response, "/home/login");
                return null;
            }
            var currentUser = AuthenticationManager.GetLoggedUser(session);
            var context = new ShouterContext();

            var notifications = context.Notifications.ToList()
                .Where(n => currentUser.Followers.Select(f => f.Id).Contains(n.Shout.AuthorId));
            var viewModel = new NotificationsListViewModel()
            {
                CurrentUser = currentUser,
                IsLoggedIn = true,
                Notifications = notifications
            };
            return View(viewModel);


        }
    }
}
