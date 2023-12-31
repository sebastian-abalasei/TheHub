﻿#region

using TheHub.Application.Common.Interfaces;
using TheHub.Application.Common.Security;
using TheHub.Domain.Constants;

#endregion

namespace TheHub.Application.TodoLists.Commands.PurgeTodoLists;

// [Authorize(Claims = Claims.Administrator)]
[Authorize(Policy = Policies.Administrator)]
public record PurgeTodoListsCommand : IRequest;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeTodoListsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _context.TodoLists.RemoveRange(_context.TodoLists);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
