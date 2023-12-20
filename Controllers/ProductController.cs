using DapperSampleNorthWind.Models.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DapperSampleNorthWind.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _ctx;

        public ProductController (IProductRepository ctx )
        {
            this._ctx = ctx;

        }

        public async Task<IActionResult> Index()
        {
            var result = await _ctx.GetAllAsync();
            return View( result );

        }

    }
}
