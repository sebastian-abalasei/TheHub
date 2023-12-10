#region

using Microsoft.AspNetCore.Identity;
using TheHub.Application.Common.Models;

#endregion

namespace TheHub.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}
