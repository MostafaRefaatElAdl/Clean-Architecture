using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OnionArch.Application.Authentication.Commands.Register;
using OnionArch.Application.Services.Authentication.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Application.Common.Behaviors
{
    public class ValidationBahavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBahavior(IValidator<TRequest>? validator =null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if(_validator is null)
            {
                
                    return await next();
                
            }
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }

            var errors = validationResult.Errors.ConvertAll(x => Error.Validation(x.PropertyName, x.ErrorMessage));

            return (dynamic)errors;
        }

    }
        
}
