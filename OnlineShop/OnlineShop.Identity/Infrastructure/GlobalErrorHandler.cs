using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using OnlineShop.Shared.Exceptions;
using OnlineShop.Shared.Response;
using System.Net;

namespace OnlineShop.Identity.Infrastructure;

public class GlobalErrorHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        Type type = exception.GetType();

        return type switch
        {
            var ex when ex == typeof(AppException) =>
            await HandleException(httpContext, exception, HttpStatusCode.BadRequest, cancellationToken),
            var ex when ex == typeof(ValidationException) =>
            await HandleException(httpContext, exception, HttpStatusCode.BadRequest, cancellationToken),
            var ex when ex == typeof(NotFoundException) =>
            await HandleException(httpContext, exception, HttpStatusCode.NotFound, cancellationToken),
            _ => await HandleException(httpContext, exception, HttpStatusCode.BadRequest, cancellationToken),
        };
    }

    async Task<bool> HandleException(HttpContext httpContext, Exception exception, HttpStatusCode code, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)code;
        var response = BaseResult.Fail(exception.Message);
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}
