#region

using TheHub.Application.Common.Interfaces;
using TheHub.Application.Questionnaires.Commands.CreateTodoList;
using TheHub.Application.TodoLists.Commands.CreateTodoList;

#endregion

namespace TheHub.Application.Questionnaires.Commands.CreateQuestionnaire;

public class CreateQuestionnaireCommandValidator : AbstractValidator<CreateQuestionnaireCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateQuestionnaireCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
            .WithMessage("'{PropertyName}' must be unique.")
            .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Questionnaires
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}
