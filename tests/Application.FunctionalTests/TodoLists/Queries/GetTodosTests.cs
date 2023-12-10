#region

using TheHub.Application.TodoLists.Queries.GetTodos;
using TheHub.Domain.Entities;
using TheHub.Domain.ValueObjects;

#endregion

namespace TheHub.Application.FunctionalTests.TodoLists.Queries;

#region

using static Testing;

#endregion

public class GetTodosTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        await RunAsDefaultUserAsync();

        GetTodosQuery query = new GetTodosQuery();

        TodosVm result = await SendAsync(query);

        result.PriorityLevels.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new TodoList
        {
            Title = "Shopping",
            Colour = Colour.Blue,
            Items =
            {
                new TodoItem { Title = "Apples", Done = true },
                new TodoItem { Title = "Milk", Done = true },
                new TodoItem { Title = "Bread", Done = true },
                new TodoItem { Title = "Toilet paper" },
                new TodoItem { Title = "Pasta" },
                new TodoItem { Title = "Tissues" },
                new TodoItem { Title = "Tuna" }
            }
        });

        GetTodosQuery query = new GetTodosQuery();

        TodosVm result = await SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().Items.Should().HaveCount(7);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        GetTodosQuery query = new GetTodosQuery();

        Func<Task<TodosVm>> action = () => SendAsync(query);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
