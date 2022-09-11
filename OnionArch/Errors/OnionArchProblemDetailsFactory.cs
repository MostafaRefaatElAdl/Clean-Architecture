using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace OnionArch.Errors
{
    public class OnionArchProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;
        public OnionArchProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }
        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            AddCustomProperties(problemDetails);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= StatusCodes.Status400BadRequest;

            var validationProblemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            if (title is not null)
            {
                validationProblemDetails.Title = title;
            }

            AddCustomProperties(validationProblemDetails);

            return validationProblemDetails;
        }

        private static void AddCustomProperties(ProblemDetails problemDetails)
        {
            problemDetails.Extensions.Add("myCustomProperty", "myCustomPropertyValue");
        }
    }
}
