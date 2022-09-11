using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnionArch.Filters
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var errorResult = new { error = "Internal Server Error from the custom Filter." };
            
            context.Result = new ObjectResult(errorResult) { StatusCode = 500 };
            base.OnException(context);
        }
    }
}
