#region

using TheHub.Application.Common.Interfaces;
using TheHub.Application.Common.Models;
using TheHub.Application.Common.Security;
using TheHub.Domain.Enums;

#endregion

namespace TheHub.Application.TodoLists.Queries.GetTodos;

[Authorize]
public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;

    public GetTodosQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
                .ToList(),
            Lists = await _context.TodoLists.Include(a=>a.Items)
                .AsNoTracking()
                .Select(s => (TodoListDto)s)
                .ToListAsync(cancellationToken)
                
        };
    }
}
