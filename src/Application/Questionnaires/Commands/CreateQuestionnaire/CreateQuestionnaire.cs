#region

using TheHub.Application.Common.Interfaces;
using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.Questionnaires.Commands.CreateTodoList;

public record CreateQuestionnaireCommand : IRequest<int>
{
    public string Title { get; init; } = String.Empty;
}

public class CreateQuestionnaireCommandHandler : IRequestHandler<CreateQuestionnaireCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateQuestionnaireCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateQuestionnaireCommand request, CancellationToken cancellationToken)
    {
        TodoList entity = new TodoList();

        entity.Title = request.Title;

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
