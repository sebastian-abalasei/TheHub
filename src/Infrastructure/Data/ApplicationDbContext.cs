#region

using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheHub.Application.Common.Interfaces;
using TheHub.Domain.Entities;
using TheHub.Domain.Quiz.Entities;
using TheHub.Infrastructure.Identity;

#endregion

namespace TheHub.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityUserContext<ApplicationUser, ulong>(options), IApplicationDbContext
{
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    
    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
