using DapperSampleNorthWind.Models.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DapperSampleNorthWind.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _orderRepository.GetAllAsync();
            return View(result);
        }

    }
}
