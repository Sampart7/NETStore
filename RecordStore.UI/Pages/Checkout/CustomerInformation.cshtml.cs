using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using System.ComponentModel;

namespace RecordStore.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public CustomerInformationModel(IConfiguration config)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
        }

        public string PublicKey { get; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if(information == null) 
            {
                return Page();
            }
            else 
            {
                return RedirectToPage("/Checkout/Payment");
            }
        }

        public IActionResult OnPost() 
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}
