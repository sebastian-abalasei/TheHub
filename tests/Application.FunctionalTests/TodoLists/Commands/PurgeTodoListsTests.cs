#region

using TheHub.Application.Common.Exceptions;
using TheHub.Application.Common.Security;
using TheHub.Application.TodoLists.Commands.CreateTodoList;
using TheHub.Application.TodoLists.Commands.PurgeTodoLists;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.FunctionalTests.TodoLists.Commands;

#region

using static Testing;

#endregion

public class PurgeTodoListsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        PurgeTodoListsCommand command = new PurgeTodoListsCommand();

        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        Func<Task> action = () => SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();

        PurgeTodoListsCommand command = new PurgeTodoListsCommand();

        Func<Task> action = () => SendAsync(command);

        await action.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldAllowAdministrator()
    {
        await RunAsAdministratorAsync();

        PurgeTodoListsCommand command = new PurgeTodoListsCommand();

        Func<Task> action = () => SendAsync(command);

        await action.Should().NotThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldDeleteAllLists()
    {
        await RunAsAdministratorAsync();

        await SendAsync(new CreateTodoListCommand { Title = "New List #1" });

        await SendAsync(new CreateTodoListCommand { Title = "New List #2" });

        await SendAsync(new CreateTodoListCommand { Title = "New List #3" });

        await SendAsync(new PurgeTodoListsCommand());

        int count = await CountAsync<TodoList>();

        count.Should().Be(0);
    }
}
