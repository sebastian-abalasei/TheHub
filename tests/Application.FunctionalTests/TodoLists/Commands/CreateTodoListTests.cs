﻿#region

using TheHub.Application.Common.Exceptions;
using TheHub.Application.TodoLists.Commands.CreateTodoList;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.FunctionalTests.TodoLists.Commands;

#region

using static Testing;

#endregion

public class CreateTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        CreateTodoListCommand command = new CreateTodoListCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<Exception>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateTodoListCommand { Title = "Shopping" });

        CreateTodoListCommand command = new CreateTodoListCommand { Title = "Shopping" };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<Exception>();
    }

    [Test]
    public async Task ShouldCreateTodoList()
    {
        ulong userId = await RunAsDefaultUserAsync();

        CreateTodoListCommand command = new CreateTodoListCommand { Title = "Tasks" };

        int id = await SendAsync(command);

        TodoList? list = await FindAsync<TodoList>(id);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
