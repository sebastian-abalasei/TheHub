﻿#region

using TheHub.Application.Common.Models;

#endregion

namespace TheHub.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
    {
        return PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
    }

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable<TDestination> queryable) where TDestination : class
    {
        return queryable.AsNoTracking().ToListAsync();
    }
}
