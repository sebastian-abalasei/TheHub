#region

using TheHub.Application.TodoItems.Commands.CreateTodoItem;
using TheHub.Application.TodoItems.Commands.DeleteTodoItem;
using TheHub.Application.TodoLists.Commands.CreateTodoList;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.FunctionalTests.TodoItems.Commands;

#region

using static Testing;

#endregion

public class DeleteTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        DeleteTodoItemCommand command = new DeleteTodoItemCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        int listId = await SendAsync(new CreateTodoListCommand { Title = "New List" });

        int itemId = await SendAsync(new CreateTodoItemCommand { ListId = listId, Title = "New Item" });

        await SendAsync(new DeleteTodoItemCommand(itemId));

        TodoItem? item = await FindAsync<TodoItem>(itemId);

        item.Should().BeNull();
    }
}
