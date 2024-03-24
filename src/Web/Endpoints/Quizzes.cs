#region

using TheHub.Application.Questionnaires.Commands.UpdateQuestionnaire;
using TheHub.Application.Quizzes.Commands.CreateQuiz;
using TheHub.Application.Quizzes.Queries.GetQuizzes;
using TheHub.Domain.Common;
using TheHub.Domain.Quiz;

#endregion

namespace TheHub.Web.Endpoints;

public class Quizzes : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetQuizzes)
            .MapGet(GetQuiz, "{id}")
            .MapPost(CreateQuiz)
            .MapPut(UpdateQuiz, "{id}")
            ;
    }

    public async Task<List<IdValueDto>> GetQuizzes(ISender sender)
    {
        return await sender.Send(new GetQuizzesQuery());
    }

    public async Task<QuizAggregate> GetQuiz(ISender sender, int id)
    {
        return await sender.Send(new GetQuizQuery(id));
    }
    
    public async Task<int> CreateQuiz(ISender sender, CreateQuizCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateQuiz(ISender sender, int id, UpdateQuizCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }

        await sender.Send(command);
        return Results.NoContent();
    }
}
