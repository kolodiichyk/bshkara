using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Bashkra.ApiClient.Responses;

namespace Bshkara.Web.Filters
{
    public class ExecutionTimeApiFilter : ActionFilterAttribute
    {
        private readonly Stopwatch _stopwatch;

        public ExecutionTimeApiFilter()
        {
            _stopwatch = new Stopwatch();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _stopwatch.Start();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _stopwatch.Stop();

            ApiResponse model = null;
            actionExecutedContext.Response.TryGetContentValue(out model);
            if (model != null)
            {
                model.ExecutionTime = _stopwatch.Elapsed;

                (actionExecutedContext.ActionContext.Response.Content as ObjectContent).Value = model;
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}