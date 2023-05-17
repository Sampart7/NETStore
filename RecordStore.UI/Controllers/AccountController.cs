using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RecordStore.UI.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _singInManager;

        public AccountController(SignInManager<IdentityUser> signInManager) 
        {
            _singInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
