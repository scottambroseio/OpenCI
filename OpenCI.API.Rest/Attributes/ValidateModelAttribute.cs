using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OpenCI.API.Rest.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsKey("model") && actionContext.ActionArguments["model"] == null)
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest);
            else if (!actionContext.ModelState.IsValid)
                actionContext.Response =
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            ;
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            OnActionExecuting(actionContext);

            return Task.CompletedTask;
        }
    }
}