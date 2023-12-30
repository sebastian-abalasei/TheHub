#region

using TheHub.Application.Common.Interfaces;
using TheHub.Application.Common.Models;
using TheHub.Application.Common.Security;
using TheHub.Domain.Constants;
using TheHub.Domain.Enums;

#endregion

namespace TheHub.Application.TodoLists.Queries.GetTodos;

[Authorize(Policy = "Administrator")]
public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTodosQuery, TodosVm>
{
    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
                .ToList(),
            Lists = await context.TodoLists.Include(a=>a.Items)
                .AsNoTracking()
                .Select(s => (TodoListDto)s)
                .ToListAsync(cancellationToken)
                
        };
    }
}
