using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Store.Shared.Exceptions;

namespace Store.Shared.Handlers;

public class GlobalExceptionHandler(IProblemDetailsService problemDetails) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problem = exception switch
        {
            FluentValidation.ValidationException validation => CreateValidationProblem(validation),
            NotFoundException => CreateProblem(StatusCodes.Status404NotFound, exception.Message),
            _ => CreateProblem(StatusCodes.Status400BadRequest, exception.Message)
        };

        httpContext.Response.StatusCode = problem.Status!.Value;

        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problem
        });
    }

    private static ProblemDetails CreateProblem(int status, string title)
    {
        return new ProblemDetails
        {
            Status = status,
            Title = title
        };
    }

    // FluentValidation: título com um erro por linha + os erros agrupados por campo
    private static ProblemDetails CreateValidationProblem(FluentValidation.ValidationException validation)
    {
        var errors = validation.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        return new HttpValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = string.Join("\n", validation.Errors.Select(e => $"• {e.ErrorMessage}"))
        };
    }
}