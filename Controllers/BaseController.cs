using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ecom.Controllers
{
    public class BaseController : Controller
    {
        // Property to check if the user is an admin
        protected bool IsAdmin
        {
            get
            {
                /*return User.IsInRole("Admin");*/
                return !string.IsNullOrEmpty(HttpContext.Session.GetString("ADMIN_ID"));
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsAdmin)
            {
                context.Result = RedirectToAction("AdminAccess","Admin");
            }
            base.OnActionExecuting(context);
        }
    }
}
