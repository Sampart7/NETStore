using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Products;
using Shop.Database;

namespace RecordStore.UI.Pages
{
    public class ProductModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;

        public ProductModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public GetProduct.ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_ctx).Do(name.Replace("-", " "));
            if(Product == null)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPost() 
        {
            var stockAdded = await new AddToCart(HttpContext.Session, _ctx).Do(CartViewModel);

            if(stockAdded)
            {
                return RedirectToPage("Cart");
            }
            else
            {
                return Page();
            }
        }
    }
}
