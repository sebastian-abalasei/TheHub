#region

using TheHub.Application.Common.Interfaces;

#endregion

namespace TheHub.Application.Questionnaires.Commands.UpdateQuestionnaire;

public class UpdateQuestionnaireCommandValidator : AbstractValidator<UpdateQuestionnaireCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuestionnaireCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
            .WithMessage("'{PropertyName}' must be unique.")
            .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(UpdateQuestionnaireCommand model, string title,
        CancellationToken cancellationToken)
    {
        return await _context.TodoLists
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}
