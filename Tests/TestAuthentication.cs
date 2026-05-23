using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using SafeVault.Models;

namespace SafeVault.Tests;

[TestFixture]
public class TestAuthentication
{
    [Test]
    public void PasswordHasher_ShouldVerifyCorrectPassword()
    {
        var user = new ApplicationUser
        {
            UserName = "test@example.com",
            Email = "test@example.com"
        };

        var hasher = new PasswordHasher<ApplicationUser>();

        string password = "Secure123!";
        string hashedPassword = hasher.HashPassword(user, password);

        var result = hasher.VerifyHashedPassword(user, hashedPassword, password);

        Assert.That(result, Is.EqualTo(PasswordVerificationResult.Success));
    }

    [Test]
    public void PasswordHasher_ShouldRejectIncorrectPassword()
    {
        var user = new ApplicationUser
        {
            UserName = "test@example.com",
            Email = "test@example.com"
        };

        var hasher = new PasswordHasher<ApplicationUser>();

        string correctPassword = "Secure123!";
        string wrongPassword = "Wrong123!";

        string hashedPassword = hasher.HashPassword(user, correctPassword);

        var result = hasher.VerifyHashedPassword(user, hashedPassword, wrongPassword);

        Assert.That(result, Is.EqualTo(PasswordVerificationResult.Failed));
    }
}