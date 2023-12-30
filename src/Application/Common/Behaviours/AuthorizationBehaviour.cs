#region

using System.Reflection;
using TheHub.Application.Common.Exceptions;
using TheHub.Application.Common.Interfaces;
using TheHub.Application.Common.Security;

#endregion

namespace TheHub.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IIdentityService _identityService;
    private readonly IUser _user;

    public AuthorizationBehaviour(
        IUser user,
        IIdentityService identityService)
    {
        _user = user;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        IEnumerable<AuthorizeAttribute> authorizeAttributes =
            request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_user.Id <= 0)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            IEnumerable<AuthorizeAttribute> authorizeAttributesWithRoles =
                authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                bool authorized = false;

                foreach (string[] roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (string role in roles)
                    {
                        bool isInRole = await _identityService.IsInRoleAsync(_user.Id, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }

            // Policy-based authorization
            IEnumerable<AuthorizeAttribute> authorizeAttributesWithPolicies =
                authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (string policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
                return await next();
            }

            // Claims-based authorization
            /*IEnumerable<AuthorizeAttribute> authorizeAttributesWithClaims =
                authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Claims));
            
            if (authorizeAttributesWithClaims.Any())
            {
                foreach (string claim in authorizeAttributesWithClaims.Select(a => a.Claims))
                {
                    authorized = await _identityService.AuthorizeAsync(_user.Id, claim);

                    if (authorized)
                    {
                        break;
                    }
                }

                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }*/
        }

        // User is authorized / authorization not required
        return await next();
    }
}
