using System.Diagnostics;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();
        var startTime = DateTime.Now.ToString("yyyyMMddHHmmss");

        var response = await next();

        _timer.Stop();
        var endTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 60000) //warning for response duration > 60 seconds
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            var userName = _currentUserService.Email ?? string.Empty;

            //if (!string.IsNullOrEmpty(userId))
            //{
            //    userName = await _identityService.GetUserNameAsync(userId);
            //}

            _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }
        if (response is Models.Response)
        {
            var _res = response as Models.Response;
            if (_res != null)
            {
                _res.RequestTime = startTime;
                _res.ResponseTime = endTime;
            }
        }
        return response;
    }
}
