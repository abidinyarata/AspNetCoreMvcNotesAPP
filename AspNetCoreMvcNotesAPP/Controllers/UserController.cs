using AspNetCoreMvcNotesAPP.Business;
using AspNetCoreMvcNotesAPP.Entities;
using AspNetCoreMvcNotesAPP.Filters;
using AspNetCoreMvcNotesAPP.ViewModels.UserModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AspNetCoreMvcNotesAPP.Controllers
{
    [LoginCheck]
    [AdminCheck]
    public class UserController : MyController
    {
        private UserService _userService = new UserService();

        public IActionResult Index()
        {
            ServiceResult<List<User>> result = _userService.ListAll();
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Create(UserCreateViewModel model)
        {
            Thread.Sleep(3000);

            ServiceResult<User> result = null;

            if (ModelState.IsValid)
            {
                result = _userService.Create(model);
                return Json(result);
            }

            result = new ServiceResult<User>();
            

            if (ModelState.ErrorCount > 0)
            {
                foreach (var item in ModelState.Values.Where(x => x.Errors.Count > 0))
                {
                    result.AddError(string.Empty, item.Errors.First().ErrorMessage);
                }
            }

            return Json(result);
        }

        public IActionResult UserList()
        {
            Thread.Sleep(3000);

            ServiceResult<List<User>> result = _userService.ListAll();
            return PartialView("_ListPartial", result.Data);
        }
    }
}
