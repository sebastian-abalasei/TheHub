#region

using TheHub.Application.Questionnaires.Commands.CreateTodoList;
using TheHub.Application.Questionnaires.Commands.UpdateQuestionnaire;
using TheHub.Application.Questionnaires.Queries.GetQuestionnaires;
using TheHub.Application.TodoLists.Commands.CreateTodoList;
using TheHub.Application.TodoLists.Commands.DeleteTodoList;
using TheHub.Application.TodoLists.Commands.UpdateTodoList;
using TheHub.Application.TodoLists.Queries.GetTodos;
using TheHub.Domain.Common;

#endregion

namespace TheHub.Web.Endpoints;

public class Questionnaires : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetQuestionnaires)
            .MapPost(CreateQuestionnaire)
            .MapPut(UpdateQuestionnaire, "{id}");
    }

    public async Task<List<IdValueDto>> GetQuestionnaires(ISender sender)
    {
        return await sender.Send(new GetQuestionnairesQuery());
    }

    public async Task<int> CreateQuestionnaire(ISender sender, CreateQuestionnaireCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateQuestionnaire(ISender sender, int id, UpdateQuestionnaireCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }

        await sender.Send(command);
        return Results.NoContent();
    }
}
