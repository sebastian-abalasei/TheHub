#region

using TheHub.Application.TodoLists.Queries.GetTodos;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.Questionnaires.Queries.GetQuestionnaires;

public class QuestionnaireDto
{
    public int Id { get; init; }

    public string? Title { get; init; }


    public IReadOnlyCollection<QuestionDto> Items { get; init; } = Array.Empty<QuestionDto>();
}
