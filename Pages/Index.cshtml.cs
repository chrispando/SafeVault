using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SafeVault.Pages;

public class IndexModel : PageModel
{
    private readonly UserRepository _userRepository;
    public IndexModel(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            string safeUsername = Services.InputValidator.ValidateAndSanitizeUsername(Username);
            string safeEmail = Services.InputValidator.ValidateAndSanitizeEmail(Email);

            await _userRepository.CreateUserAsync(safeUsername, safeEmail);

            Message = "Demo user record saved successfully.";
            return Page();
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            Message = ex.Message;
            return Page();
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while saving the user.");
            Message = "An error occurred while saving the user.";
            return Page();
        }
    }
}