using NUnit.Framework;
using SafeVault.Services;

namespace SafeVault.Tests;

[TestFixture]
public class TestSecurityFixes
{
    [Test]
    public void Username_ShouldRejectSqlInjectionPayload()
    {
        string payload = "admin' OR '1'='1";

        Assert.Throws<ArgumentException>((Action)(() =>
            InputValidator.ValidateAndSanitizeUsername(payload)));
    }

    [Test]
    public void Username_ShouldRejectDropTablePayload()
    {
        string payload = "admin; DROP TABLE Users; --";

        Assert.Throws<ArgumentException>((Action)(() =>
            InputValidator.ValidateAndSanitizeUsername(payload)));
    }

    [Test]
    public void Username_ShouldRejectXssScriptPayload()
    {
        string payload = "<script>alert('xss')</script>";

        Assert.Throws<ArgumentException>((Action)(() =>
            InputValidator.ValidateAndSanitizeUsername(payload)));
    }

    [Test]
    public void Username_ShouldRejectHtmlImageXssPayload()
    {
        string payload = "<img src=x onerror=alert(1)>";

        Assert.Throws<ArgumentException>((Action)(() =>
            InputValidator.ValidateAndSanitizeUsername(payload)));
    }

    [Test]
    public void Email_ShouldRejectSqlInjectionPayload()
    {
        string payload = "test@example.com'; DROP TABLE Users; --";

        Assert.Throws<ArgumentException>((Action)(() =>
            InputValidator.ValidateAndSanitizeEmail(payload)));
    }

    [Test]
    public void HtmlEncoding_ShouldEncodeScriptTags()
    {
        string payload = "<script>alert('xss')</script>";

        string encoded = InputValidator.EncodeForHtmlDisplay(payload);

        Assert.That(encoded, Does.Contain("&lt;script&gt;"));
        Assert.That(encoded, Does.Contain("&lt;/script&gt;"));
    }

    [Test]
    public void HtmlEncoding_ShouldEncodeImageXssPayload()
    {
        string payload = "<img src=x onerror=alert(1)>";

        string encoded = InputValidator.EncodeForHtmlDisplay(payload);

        Assert.That(encoded, Does.Contain("&lt;img"));
        Assert.That(encoded, Does.Contain("onerror"));
        Assert.That(encoded, Does.Contain("&gt;"));
    }
}