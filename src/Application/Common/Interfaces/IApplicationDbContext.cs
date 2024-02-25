#region

using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Quiz> Quizzes { get; }
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
