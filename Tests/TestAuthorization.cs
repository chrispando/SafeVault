using System.Security.Claims;
using NUnit.Framework;
using SafeVault.Services;

namespace SafeVault.Tests;

[TestFixture]
public class TestAuthorization
{
    [Test]
    public void IsAdmin_ShouldReturnTrue_ForAdminUser()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "admin@safevault.com"),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(claims, "TestAuth");
        var user = new ClaimsPrincipal(identity);

        bool result = AuthorizationService.IsAdmin(user);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsAdmin_ShouldReturnFalse_ForRegularUser()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "user@safevault.com"),
            new Claim(ClaimTypes.Role, "User")
        };

        var identity = new ClaimsIdentity(claims, "TestAuth");
        var user = new ClaimsPrincipal(identity);

        bool result = AuthorizationService.IsAdmin(user);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsAdmin_ShouldReturnFalse_ForUnauthenticatedUser()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        bool result = AuthorizationService.IsAdmin(user);

        Assert.That(result, Is.False);
    }
}