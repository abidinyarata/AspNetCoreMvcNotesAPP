using AspNetCoreMvcNotesAPP.Business;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreMvcNotesAPP.Controllers
{
    public class MyController : Controller
    {
        public void AddErrorsToModelState(List<ErrorItem> errors)
        {
            foreach (ErrorItem item in errors)
            {
                ModelState.AddModelError(item.Key, item.Value);
            }
        }
    }
}
