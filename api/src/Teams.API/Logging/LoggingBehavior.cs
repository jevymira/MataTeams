using MediatR;

namespace Teams.API.Logging;

/// <remarks>
/// Adapted from official MediatR documentation at
/// https://github.com/LuckyPennySoftware/MediatR/wiki/Behaviors
/// and the MS eShop reference application at
/// https://github.com/dotnet/eShop/blob/main/src/Ordering.API/Application/Behaviors/LoggingBehavior.cs
/// </remarks>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");
        var response = await next();
        _logger.LogInformation($"Handled request - response: {typeof(TResponse).Name}");

        return response;
    }
}