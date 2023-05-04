using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using System.ComponentModel;

namespace RecordStore.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private IWebHostEnvironment _env;

        public CustomerInformationModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public string PublicKey { get; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if(information == null) 
            {
                if(_env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        FirstName = "Cusomer Name",
                        LastName = "Cusomer Lastname",
                        Email = "example@chester.com",
                        PhoneNumber = "420692137",
                        Addres1 = "Zary",
                        Addres2 = "Zagan",
                        City = "Zielona Gora",
                        PostCode = "67-400",
                    };
                }
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
