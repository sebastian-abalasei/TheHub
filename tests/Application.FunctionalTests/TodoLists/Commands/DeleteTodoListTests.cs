#region

using TheHub.Application.TodoLists.Commands.CreateTodoList;
using TheHub.Application.TodoLists.Commands.DeleteTodoList;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.FunctionalTests.TodoLists.Commands;

#region

using static Testing;

#endregion

public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        DeleteTodoListCommand command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        int listId = await SendAsync(new CreateTodoListCommand { Title = "New List" });

        await SendAsync(new DeleteTodoListCommand(listId));

        TodoList? list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
