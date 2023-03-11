using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Ojaile.Webapi.Helpers
{
    public class LogAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            LogData("onResultExecuted", context.RouteData);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LogData("onActionExecuting", context.RouteData);
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            LogData("onResultExecuted", context.RouteData);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            LogData("onResultExecuting", context.RouteData);
        }
        private void LogData(string methodString, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} => Controller: {1} action: {2}", methodString, controllerName, actionName);
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }
    }

    
}
