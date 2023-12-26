#region

using System.Security.Claims;
using TheHub.Application.Common.Interfaces;

#endregion

namespace TheHub.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ulong Id
    {
        get
        {
            var t = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (t != null)
            {
                return ulong.Parse(t);
            }
            else
            {
                return 0;
            }
        }
    }
}
