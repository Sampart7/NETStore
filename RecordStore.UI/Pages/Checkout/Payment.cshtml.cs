using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Shop.Application.Cart;
using Stripe;

namespace RecordStore.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public PaymentModel(IConfiguration config)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
        }

        public string PublicKey { get; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if (information == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }

        public IActionResult OnPost(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email= stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 200,
                Description = "Simple charge",
                Currency = "pln",
                Customer = customer.Id,
            });

            return RedirectToPage("/Index");
        }
    }
}
