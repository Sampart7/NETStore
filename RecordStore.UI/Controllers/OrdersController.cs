using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using Shop.Database;

namespace RecordStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class OrdersController : Controller
    {
        private ApplicationDbContext _ctx;

        public OrdersController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        //[HttpGet("")]
        // public IActionResult GetOrders(int status) => Ok(new GetOrder(_ctx).Do(status));

       // [HttpGet("{id}")]
        //  public IActionResult GetOrder(int id) => Ok(new GetOrder(_ctx).Do(id));

       // [HttpPost("{id}")]
        //  public async Task<IActionResult> UpdateOrder(int id) => Ok((await new UpdateOrder(_ctx).Do(id)));
    }
}
