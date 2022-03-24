namespace HeroicBrawlServer.Controllers.Middleware
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var response = new HttpResponseMessage();

            if (actionExecutedContext.Exception.InnerException == null)
            {
                response.Content = new StringContent(actionExecutedContext.Exception.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                response.Content = new StringContent($"{actionExecutedContext.Exception.Message} - {actionExecutedContext.Exception.InnerException.Message}");
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            response.Content = new StringContent("Unknown error");
            response.StatusCode = HttpStatusCode.InternalServerError;

            actionExecutedContext.Response = response;
        }
    }
}
