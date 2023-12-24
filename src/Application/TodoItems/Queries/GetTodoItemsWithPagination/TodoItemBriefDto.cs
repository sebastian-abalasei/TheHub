#region

using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    public static explicit operator TodoItemBriefDto(TodoItem item)
    {
        return new TodoItemBriefDto()
        {
            Id = item.Id,
            Title = item.Title,
            Done = item.Done,
            ListId = item.ListId
        };
    }
}
