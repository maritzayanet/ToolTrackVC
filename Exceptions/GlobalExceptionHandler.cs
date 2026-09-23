using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ToolTrack.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ToolNotFoundException)
        {
            _logger.LogWarning(
                exception,
                "No se encontró el recurso solicitado."
            );

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Tool not found",
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = 404;

            await httpContext.Response.WriteAsJsonAsync(
                problemDetails,
                cancellationToken
            );

            return true;
        }

        _logger.LogError(
            exception,
            "Ocurrió una excepción no controlada."
        );

        var serverError = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = "Ocurrió un error inesperado.",
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = 500;

        await httpContext.Response.WriteAsJsonAsync(
            serverError,
            cancellationToken
        );

        return true;
    }
}