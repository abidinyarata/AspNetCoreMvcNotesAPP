using AspNetCoreMvcNotesAPP.Business;
using AspNetCoreMvcNotesAPP.Core;
using AspNetCoreMvcNotesAPP.Entities;
using AspNetCoreMvcNotesAPP.Filters;
using AspNetCoreMvcNotesAPP.Models;
using AspNetCoreMvcNotesAPP.ViewModels.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreMvcNotesAPP.Controllers
{
    public class HomeController : MyController
    {
        private UserService _userService = new UserService();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<User> result = _userService.Login(model);

                if (result.HasError)
                {
                    AddErrorsToModelState(result.Errors);
                }
                else
                {
                    HttpContext.Session.SetString(Constants.SessionUsername, result.Data.Username);
                    HttpContext.Session.SetInt32(Constants.SessionUserId, result.Data.Id);
                    HttpContext.Session.SetString(Constants.SessionUserEmail, result.Data.Email);
                    HttpContext.Session.SetString(Constants.SessionUserRole, result.Data.IsAdmin ? Constants.RoleAdmin : Constants.RoleMember);

                    return RedirectToAction(nameof(HomeController.Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<User> result = _userService.Register(model);

                if (result.HasError)
                {
                    AddErrorsToModelState(result.Errors);
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [LoginCheck]
        public IActionResult ShowProfile()
        {
            int userId = HttpContext.Session.GetInt32(Constants.SessionUserId).Value;
            ServiceResult<User> result = _userService.Find(userId);

            if (result.HasError)
            {
                return RedirectToAction(nameof(Login));
            }
            return View(result.Data);
        }

        [LoginCheck]
        public IActionResult EditProfile()
        {
            return View();
        }

        [LoginCheck]
        public IActionResult DeleteProfile()
        {
            return View();
        }

        [LoginCheck]
        public new IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Unauthorize()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
