using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shopping_System.DTO;
using Online_Shopping_System.Helper;
using Online_Shopping_System.IRepository;

namespace Online_Shopping_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _IRepository;

        public OrderController(IOrder IRepository)
        {
            _IRepository= IRepository;
        }

        [HttpPost]
        [Route("CheckOut")]

        public async Task<IActionResult> CheckOut(long shoppingCartId)
        {
            var msg = await _IRepository.CheckOut(shoppingCartId);
            return Ok(msg);
        }

        [HttpGet]
        [Route("OrderDetails")]
        public async Task<IActionResult> OrderDetails(long OrderId)
        {
            var dt = await _IRepository.OrderDetails(OrderId);
            return Ok(JsonConvert.SerializeObject(dt));
        }
    }
}
