﻿#region

using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using TheHub.Application.Common.Interfaces;

#endregion

namespace TheHub.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IIdentityService _identityService;
    private readonly ILogger _logger;
    private readonly IUser _user;

    public LoggingBehaviour(ILogger<TRequest> logger, IUser user, IIdentityService identityService)
    {
        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        string userId = _user.Id ?? string.Empty;
        string? userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        _logger.LogInformation("TheHub Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
