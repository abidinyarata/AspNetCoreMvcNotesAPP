using AspNetCoreMvcNotesAPP.Business;
using AspNetCoreMvcNotesAPP.Entities;
using AspNetCoreMvcNotesAPP.Filters;
using AspNetCoreMvcNotesAPP.ViewModels.CategoryModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreMvcNotesAPP.Controllers
{
    [LoginCheck]
    [AdminCheck]
    public class CategoryController : MyController
    {
        private CategoryService categoryService = new CategoryService();

        public IActionResult Index()
        {
            ServiceResult<List<Category>> result = categoryService.ListAll();
            return View(result.Data);
        }

        public IActionResult Details(int id)
        {
            ServiceResult<Category> result = categoryService.Find(id);
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<Category> result = categoryService.Create(model);

                if (result.HasError)
                {
                    AddErrorsToModelState(result.Errors);
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            ServiceResult<Category> result = categoryService.Find(id);

            if (result.HasError)
            {
                AddErrorsToModelState(result.Errors);

                if (result.NotFound)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            CategoryEditViewModel model = null;

            if (result.Data != null)
            {
                model = new CategoryEditViewModel
                {
                    Name = result.Data.Name,
                    Desc = result.Data.Desc,
                    IsHidden = result.Data.IsHidden,
                    NotFound = result.NotFound
                };
            }
            else
            {
                model = new CategoryEditViewModel
                {
                    NotFound = result.NotFound
                };
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryEditViewModel model)
        {
            ServiceResult<Category> result = categoryService.Update(id, model);

            if (result.HasError)
            {
                AddErrorsToModelState(result.Errors);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ServiceResult<Category> result = categoryService.Find(id);
            return View(result.Data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            ServiceResult<object> result = categoryService.Remove(id);

            if (result.HasError)
            {
                AddErrorsToModelState(result.Errors);
                return View(nameof(Delete), categoryService.Find(id).Data);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
