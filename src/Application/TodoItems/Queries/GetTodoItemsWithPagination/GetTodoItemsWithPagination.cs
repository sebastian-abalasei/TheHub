﻿#region

using TheHub.Application.Common.Interfaces;
using TheHub.Application.Common.Mappings;
using TheHub.Application.Common.Models;

#endregion

namespace TheHub.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class
    GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery,
    PaginatedList<TodoItemBriefDto>>
{
    private readonly IApplicationDbContext _context;
    

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .Select(i=>(TodoItemBriefDto)i)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
