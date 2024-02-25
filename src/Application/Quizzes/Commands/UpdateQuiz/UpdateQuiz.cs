#region

using TheHub.Application.Common.Interfaces;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.Questionnaires.Commands.UpdateQuestionnaire;

public record UpdateQuestionnaireCommand : IRequest
{
    public int Id { get; init; }

    public required string Title { get; init; }
}

public class UpdateQuestionnaireCommandHandler : IRequestHandler<UpdateQuestionnaireCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuestionnaireCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateQuestionnaireCommand request, CancellationToken cancellationToken)
    {
        TodoList? entity = await _context.TodoLists
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
