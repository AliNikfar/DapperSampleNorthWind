using Microsoft.AspNetCore.Mvc;

namespace DapperSampleNorthWind.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
