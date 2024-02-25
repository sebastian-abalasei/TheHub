#region

using TheHub.Application.Questionnaires.Commands.UpdateQuestionnaire;
using TheHub.Application.Questionnaires.Queries.GetQuestionnaires;
using TheHub.Application.Quizzes.Commands.CreateQuiz;
using TheHub.Domain.Common;

#endregion

namespace TheHub.Web.Endpoints;

public class Quizzes : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetQuizzes)
            .MapPost(CreateQuiz)
            .MapPut(UpdateQuiz, "{id}");
    }

    public async Task<List<IdValueDto>> GetQuizzes(ISender sender)
    {
        return await sender.Send(new GetQuestionnairesQuery());
    }

    public async Task<int> CreateQuiz(ISender sender, CreateQuizCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateQuiz(ISender sender, int id, UpdateQuestionnaireCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }

        await sender.Send(command);
        return Results.NoContent();
    }
}
