#region

using TheHub.Application.Common.Exceptions;
using TheHub.Application.TodoLists.Commands.CreateTodoList;
using TheHub.Application.TodoLists.Commands.UpdateTodoList;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.FunctionalTests.TodoLists.Commands;

#region

using static Testing;

#endregion

public class UpdateTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        UpdateTodoListCommand command = new UpdateTodoListCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoList()
    {
        ulong userId = await RunAsDefaultUserAsync();

        int listId = await SendAsync(new CreateTodoListCommand { Title = "New List" });

        UpdateTodoListCommand command = new UpdateTodoListCommand { Id = listId, Title = "Updated List Title" };

        await SendAsync(command);

        TodoList? list = await FindAsync<TodoList>(listId);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.LastModifiedBy.Should().NotBe(0);
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
