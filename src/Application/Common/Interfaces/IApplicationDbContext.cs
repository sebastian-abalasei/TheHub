#region

using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Questionnaire> Questionnaires { get; }
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
