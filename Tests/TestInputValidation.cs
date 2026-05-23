using NUnit.Framework;
using SafeVault.Services;

namespace SafeVault.Tests;

[TestFixture]
public class TestInputValidation
{
    [Test]
    public void TestForSQLInjection_InUsername_ShouldBeRejected()
    {
        string maliciousUsername = "' OR '1'='1";

        Assert.Throws<ArgumentException>((Action)(() =>
            InputValidator.ValidateAndSanitizeUsername(maliciousUsername)));
    }

    [Test]
    public void TestForSQLInjection_InEmail_ShouldBeRejected()
    {
        string maliciousEmail = "test@example.com'; DROP TABLE Users; --";

        Assert.Throws<ArgumentException>((Action)(() =>
            InputValidator.ValidateAndSanitizeEmail(maliciousEmail)));
    }

    [Test]
    public void TestForXSS_InUsername_ShouldBeRejected()
    {
        string maliciousUsername = "<script>alert('xss')</script>";

        Assert.Throws<ArgumentException>((Action)(() =>
            InputValidator.ValidateAndSanitizeUsername(maliciousUsername)));
    }
}