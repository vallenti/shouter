using SimpleHttpServer.Models;
using SimpleMVC.App.BindingModels;
using SimpleMVC.App.Data;
using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System.Linq;

namespace SimpleMVC.App.Controllers
{
    public class FollowersController : BaseController
    {
        [HttpGet]
        public IActionResult<FeedViewModel> My(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session))
            {
                Redirect(response, "/home/login");
                return null;
            }

            var user = AuthenticationManager.GetLoggedUser(session);
            var context = new ShouterContext();
            user = context.Users.Find(user.Id);
            var shouts = user.Followers.SelectMany(x => x.Shouts)
                .OrderByDescending(x => x.PublishedOn);
            var viewModel = new FeedViewModel()
            {
                CurrentUser = user,
                Shouts = shouts
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id, HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session))
            {
                Redirect(response, "/home/login");
                return null;
            }

            using (var context = new ShouterContext())
            {
                var user = context.Users.Find(id);
                var viewModel = new UserProfileViewModel()
                {
                    CurrentUser = user,
                    Shouts = user.Shouts.OrderByDescending(x => x.PublishedOn)
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(DeleteShoutBindingModel model, HttpSession session, HttpResponse response)
        {
            using (var context = new ShouterContext())
            {
                var shout = context.Shouts.Find(model.ShoutId);
                context.Shouts.Remove(shout);
                context.SaveChanges();
            }
            return Profile(model.UserId, session, response);
        }

        [HttpGet]
        public IActionResult<UsersListViewModel> All(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session))
            {
                Redirect(response, "/home/login");
                return null;
            }
            var viewModel = GetUsers(session);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult<UsersListViewModel> All(SearchUsersBindingModel model, HttpSession session)
        {
            var viewModel = GetUsers(session, model.Keyword);
            return View(viewModel);
        }

        [HttpPost]
        public void Follow(FollowUserBindingModel model, HttpSession session, HttpResponse response)
        {
            using (var context = new ShouterContext())
            {
                var user = context.Users.Find(model.CurrentUserId);
                var follower = context.Users.Find(model.FollowerId);
                user.Followers.Add(follower);
                context.SaveChanges();
            }
            Redirect(response, "/followers/all");
        }

        [HttpPost]
        public void Unfollow(FollowUserBindingModel model, HttpSession session, HttpResponse response)
        {
            using (var context = new ShouterContext())
            {
                var user = context.Users.Find(model.CurrentUserId);
                var follower = context.Users.Find(model.FollowerId);
                user.Followers.Remove(follower);
                context.SaveChanges();
            }
            Redirect(response, "/followers/all");
        }
        [HttpGet]
        public IActionResult<UsersListViewModel> Following(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session))
            {
                Redirect(response, "/home/login");
                return null;
            }
            var viewModel = GetFollowers(session);
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult<UsersListViewModel> Following(HttpSession session, HttpResponse response, SearchUsersBindingModel model)
        {
            if (!AuthenticationManager.IsAuthenticated(session))
            {
                Redirect(response, "/home/login");
                return null;
            }
            var viewModel = GetFollowers(session, model.Keyword);
            return View(viewModel);
        }
        private UsersListViewModel GetUsers(HttpSession session, string keyword = "")
        {
            var currentUser = AuthenticationManager.GetLoggedUser(session);
            var context = new ShouterContext();
            var users = context.Users.Where(x => x.Username.Contains(keyword)).ToList();
            users.Remove(currentUser);
            var viewModel = new UsersListViewModel()
            {
                Users = users,
                CurrentUser = currentUser
            };
            return viewModel;
        }
        private UsersListViewModel GetFollowers(HttpSession session, string keyword = "")
        {
            var currentUser = AuthenticationManager.GetLoggedUser(session);
            var context = new ShouterContext();
            var user = context.Users.Find(currentUser.Id);
            var users = user.Followers.Where(x => x.Username.Contains(keyword)).ToList();
            var viewModel = new UsersListViewModel()
            {
                Users = users,
                CurrentUser = currentUser
            };
            return viewModel;
        }

    }
}
