﻿#region

using TheHub.Application.TodoItems.Commands.CreateTodoItem;
using TheHub.Application.TodoItems.Commands.UpdateTodoItem;
using TheHub.Application.TodoItems.Commands.UpdateTodoItemDetail;
using TheHub.Application.TodoLists.Commands.CreateTodoList;
using TheHub.Domain.Entities;
using TheHub.Domain.Enums;

#endregion

namespace TheHub.Application.FunctionalTests.TodoItems.Commands;

#region

using static Testing;

#endregion

public class UpdateTodoItemDetailTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        UpdateTodoItemCommand command = new UpdateTodoItemCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        string userId = await RunAsDefaultUserAsync();

        int listId = await SendAsync(new CreateTodoListCommand { Title = "New List" });

        int itemId = await SendAsync(new CreateTodoItemCommand { ListId = listId, Title = "New Item" });

        UpdateTodoItemDetailCommand command = new UpdateTodoItemDetailCommand
        {
            Id = itemId, ListId = listId, Note = "This is the note.", Priority = PriorityLevel.High
        };

        await SendAsync(command);

        TodoItem? item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Note.Should().Be(command.Note);
        item.Priority.Should().Be(command.Priority);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
