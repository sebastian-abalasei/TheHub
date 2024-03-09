#region

using TheHub.Application.Common.Interfaces;
using TheHub.Application.Common.Security;
using TheHub.Domain.Common;

#endregion

namespace TheHub.Application.Quizzes.Queries.GetQuizzes;

[Authorize(Policy = "Administrator")]
public record GetQuizzesQuery : IRequest<List<IdValueDto>>;

public class GetQuestionnairesQueryHandler(IApplicationDbContext context) : IRequestHandler<GetQuizzesQuery,
    List<IdValueDto>>
{
    public async Task<List<IdValueDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        return await context.Quizzes
            .AsNoTracking()
            .Select(s => new IdValueDto(s.Id, s.Title) )
            .ToListAsync(cancellationToken);

    }
}
