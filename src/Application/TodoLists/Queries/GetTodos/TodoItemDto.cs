#region

using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.TodoLists.Queries.GetTodos;

public class TodoItemDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    public int Priority { get; init; }

    public string? Note { get; init; }

    public static explicit operator TodoItemDto(TodoItem item)
    {
        return new TodoItemDto()
        {
            Id = item.Id,
            Title = item.Title,
            Note = item.Note,
            Done = item.Done,
            Priority = (int)item.Priority,
            ListId = item.ListId
        };
    }
}
