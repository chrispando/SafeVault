using System.Net;
using System.Text.RegularExpressions;

namespace SafeVault.Services;

public static class InputValidator
{
    public static string ValidateAndSanitizeUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username is required.");
        }

        username = username.Trim();

        if (username.Length < 3 || username.Length > 50)
        {
            throw new ArgumentException("Username must be between 3 and 50 characters.");
        }

        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_.-]+$"))
        {
            throw new ArgumentException("Username contains invalid characters.");
        }

        return username;
    }

    public static string ValidateAndSanitizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required.");
        }

        email = email.Trim();

        if (email.Length > 100)
        {
            throw new ArgumentException("Email cannot exceed 100 characters.");
        }

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new ArgumentException("Invalid email format.");
        }

        return email;
    }

    public static string EncodeForHtmlDisplay(string input)
    {
        return WebUtility.HtmlEncode(input);
    }
}