using System.Security.Claims;

namespace SafeVault.Services;

public static class AuthorizationService
{
    public static bool IsAdmin(ClaimsPrincipal user)
    {
        return user.Identity?.IsAuthenticated == true &&
               user.IsInRole("Admin");
    }
}