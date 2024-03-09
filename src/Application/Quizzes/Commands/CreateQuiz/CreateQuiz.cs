#region

using TheHub.Application.Common.Interfaces;
using TheHub.Domain.Entities;
using TheHub.Domain.Quiz.Entities;

#endregion

namespace TheHub.Application.Quizzes.Commands.CreateQuiz;

public record CreateQuizCommand : IRequest<int>
{
    public string Title { get; init; } = String.Empty;
}

public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateQuizCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        Quiz entity = new Quiz(request.Title) ;

        _context.Quizzes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
