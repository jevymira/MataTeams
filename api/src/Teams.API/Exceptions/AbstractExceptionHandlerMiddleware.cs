using System.Net;

namespace Teams.API.Exceptions;

/// <remarks>
/// Adapted in part from:
/// https://medium.com/geekculture/exception-middleware-in-net-core-applications-84e0cc2dacbf
/// </remarks>
public abstract class AbstractExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Gets HTTP status code response and message to be returned to the caller.
    /// </summary>
    /// <param name="exception">The actual exception</param>
    /// <returns>Tuple of HTTP status code and a message</returns>
    public abstract (HttpStatusCode code, string message) GetResponse(Exception exception);

    public AbstractExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            
            var (status, message) = GetResponse(exception);
            response.StatusCode = (int) status;
            await response.WriteAsync(message);
        }
    }
}