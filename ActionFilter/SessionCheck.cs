using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QLDV.ActionFilter
{
    public class CheckSession : ActionFilterAttribute, IActionFilter
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = filterContext.HttpContext;

            //if Session == null => Login page
            if (ctx.Session.GetString("Rank") == null)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }


}
