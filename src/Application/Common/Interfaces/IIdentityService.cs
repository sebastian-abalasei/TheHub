#region

using TheHub.Application.Common.Models;

#endregion

namespace TheHub.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(ulong userId);

    Task<bool> IsInRoleAsync(ulong userId, string role);

    Task<bool> AuthorizeAsync(ulong userId, string policyName);

    Task<(Result Result, ulong UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(ulong userId);
}
