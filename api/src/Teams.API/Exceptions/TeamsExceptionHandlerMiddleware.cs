using System.Net;
using System.Text.Json;

namespace Teams.API.Exceptions;

public class TeamsExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
{
    public TeamsExceptionHandlerMiddleware(RequestDelegate next): base(next) { }

    public override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        HttpStatusCode code;
        switch (exception)
        {
            case KeyNotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException:
                code = HttpStatusCode.Forbidden;
                break;
            case InvalidOperationException:
                code = HttpStatusCode.Conflict;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                break;
        }
        return (code, JsonSerializer.Serialize(exception.Message));
    }
}