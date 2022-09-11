using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace OnionArch.Filters
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "An error oocured while proccessing your request.",
                Status = (int)HttpStatusCode.InternalServerError,
            };
            
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
            base.OnException(context);
        }
    }
}
