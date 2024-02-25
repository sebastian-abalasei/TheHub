#region

using TheHub.Application.Common.Interfaces;
using TheHub.Application.Common.Security;
using TheHub.Domain.Common;

#endregion

namespace TheHub.Application.Questionnaires.Queries.GetQuestionnaires;

[Authorize(Policy = "Administrator")]
public record GetQuestionnairesQuery : IRequest<List<IdValueDto>>;

public class GetQuestionnairesQueryHandler(IApplicationDbContext context) : IRequestHandler<GetQuestionnairesQuery,
    List<IdValueDto>>
{
    public async Task<List<IdValueDto>> Handle(GetQuestionnairesQuery request, CancellationToken cancellationToken)
    {
        return await context.Quizzes
            .AsNoTracking()
            .Select(s => new IdValueDto(s.Id, s.Title) )
            .ToListAsync(cancellationToken);

    }
}
