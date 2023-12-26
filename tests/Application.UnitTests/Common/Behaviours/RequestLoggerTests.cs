#region

using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TheHub.Application.Common.Behaviours;
using TheHub.Application.Common.Interfaces;
using TheHub.Application.TodoItems.Commands.CreateTodoItem;

#endregion

namespace TheHub.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<IIdentityService> _identityService = null!;
    private Mock<ILogger<CreateTodoItemCommand>> _logger = null!;
    private Mock<IUser> _user = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateTodoItemCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(1);

        LoggingBehaviour<CreateTodoItemCommand> requestLogger =
            new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<ulong>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        LoggingBehaviour<CreateTodoItemCommand> requestLogger =
            new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<ulong>()), Times.Never);
    }
}
