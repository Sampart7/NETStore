using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace RecordStore.UI.Pages.Accounts
{
    public class loginModel : PageModel
    {
        private SignInManager<IdentityUser> _singInManager;

        public loginModel(SignInManager<IdentityUser> signInManager) 
        {
            _singInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _singInManager.PasswordSignInAsync(Input.Username, Input.Password, false, false);
            
            if (result.Succeeded)
            {
                return RedirectToPage("/Admin/Index");
            }
            else
            {
                return Page();
            }
        }
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
