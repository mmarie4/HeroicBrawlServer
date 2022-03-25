using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HeroicBrawlServer.Controllers.Middleware
{

    public class ExceptionFilter : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            var errorMessage = context.Exception.InnerException == null
                               ? context.Exception.Message
                               : $"{context.Exception.Message} - {context.Exception.InnerException.Message}";

            
            context.Result = new ObjectResult(new ErrorResponse(errorMessage))
            {
                StatusCode = 500,
            };
        }
    }
}
