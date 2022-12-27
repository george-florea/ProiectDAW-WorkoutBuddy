using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using Backend.Common.Exceptions;

namespace Backend.WebApp.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilterAttribute> logger;

        public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            switch (context.Exception)
            {
                case NotFoundErrorException notFound:
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };

                    break;
                case UnauthorizedAccessException unauthorizedAccess:
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                    break;
                case ValidationErrorException validationError:
                    foreach (var validationResult in validationError.ValidationResult.Errors)
                    {
                        context.ModelState.AddModelError(validationResult.PropertyName, validationResult.ErrorMessage);
                    }

                    context.Result = new ObjectResult(validationError.ValidationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }))
                    {
                        StatusCode = StatusCodes.Status412PreconditionFailed
                    };
                    break;
                default:
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;

            }
        }
    }
}
