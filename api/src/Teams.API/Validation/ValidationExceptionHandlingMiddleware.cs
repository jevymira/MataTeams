using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Teams.API.Validation;

/// <remarks>
/// Adapted from: https://www.milanjovanovic.tech/blog/cqrs-validation-with-mediatr-pipeline-and-fluentvalidation
/// </remarks>
internal sealed class ValidationExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors occurred.",
            };

            if (exception.Errors.Any())
            {
                details.Extensions["errors"] = exception.Errors;
            }
            
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(details);
        }
        
        
    }
}