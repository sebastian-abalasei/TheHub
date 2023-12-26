#region

using System.Collections.ObjectModel;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.TodoLists.Queries.GetTodos;

public class TodoListDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Colour { get; init; }

    public IReadOnlyCollection<TodoItemDto> Items { get; init; } = Array.Empty<TodoItemDto>();

    public static explicit operator TodoListDto(TodoList list)
    {
        return new TodoListDto()
        {
            Id = list.Id,
            Title = list.Title,
            Colour = list.Colour,
            Items = list.Items.Select(i => (TodoItemDto)i).ToList()
            
        };
    }
}
