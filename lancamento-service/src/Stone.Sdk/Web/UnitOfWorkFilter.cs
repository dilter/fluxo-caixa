using Microsoft.AspNetCore.Mvc.Filters;
using Stone.Sdk.Persistence;

namespace Stone.Sdk.Web
{
    public class UnitOfWorkFilter : IActionFilter
    {        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var uow = (IUnitOfWork) context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
            uow.Commit();
            uow.Dispose();
        }
    }
}