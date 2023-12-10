#region

using TheHub.Application.Common.Exceptions;
using TheHub.Application.TodoItems.Commands.CreateTodoItem;
using TheHub.Application.TodoLists.Commands.CreateTodoList;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.FunctionalTests.TodoItems.Commands;

#region

using static Testing;

#endregion

public class CreateTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        CreateTodoItemCommand command = new CreateTodoItemCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoItem()
    {
        string userId = await RunAsDefaultUserAsync();

        int listId = await SendAsync(new CreateTodoListCommand { Title = "New List" });

        CreateTodoItemCommand command = new CreateTodoItemCommand { ListId = listId, Title = "Tasks" };

        int itemId = await SendAsync(command);

        TodoItem? item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Title.Should().Be(command.Title);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
