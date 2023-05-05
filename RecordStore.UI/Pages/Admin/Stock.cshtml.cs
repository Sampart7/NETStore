using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;
using Shop.Database;
using static Shop.Application.Products.GetProducts;

namespace RecordStore.UI.Pages.Admin
{
    public class StockModel : PageModel
    {
        private ApplicationDbContext _ctx;

        public StockModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public void OnGet()
        {
            Products = new GetProducts(_ctx).Do();
        }
    }
}
