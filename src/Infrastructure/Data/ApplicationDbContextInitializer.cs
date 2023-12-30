#region

using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheHub.Domain.Constants;
using TheHub.Domain.Entities;
using TheHub.Infrastructure.Identity;

#endregion

namespace TheHub.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        ApplicationDbContextInitializer initializer =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitialiseAsync();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
     
        var administrator =
            new ApplicationUser
            {
                Email = "administrator@localhost",
                UserName = "administrator@localhost"
            };
        var claimAdmin = new Claim(Claims.Administrator, "true", ClaimValueTypes.Boolean);
        var claimRead = new Claim(Claims.AdminRead, "true", ClaimValueTypes.Boolean);
        var claimWrite = new Claim(Claims.AdminWrite, "true", ClaimValueTypes.Boolean);
        var claimDelete = new Claim(Claims.AdminDelete, "true", ClaimValueTypes.Boolean);

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            await userManager.AddClaimsAsync(administrator,
                new List<Claim> { claimAdmin, claimRead,claimWrite,claimDelete }.AsEnumerable());
        }

        var user =
            new ApplicationUser
            {
                Email = "user@localhost", UserName = "user@localhost"
            };
        var claimUser = new Claim(Claims.User, "true", ClaimValueTypes.Boolean);
        claimRead = new Claim(Claims.UserRead, "true", ClaimValueTypes.Boolean);
        claimWrite = new Claim(Claims.UserWrite, "true", ClaimValueTypes.Boolean);
        claimDelete = new Claim(Claims.UserDelete, "true", ClaimValueTypes.Boolean);

        if (userManager.Users.All(u => u.UserName != user.UserName))
        {
            await userManager.CreateAsync(user, "User1!");
            await userManager.AddClaimsAsync(user,
                new List<Claim> { claimUser,claimRead,claimWrite,claimDelete }.AsEnumerable());
        }

        // Default data
        // Seed, if necessary
        if (!context.TodoLists.Any())
        {
            context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯" },
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" }
                }
            });

            await context.SaveChangesAsync();
        }
    }
}
