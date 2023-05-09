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
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCart _IRepository;

        public ShoppingCartController(IShoppingCart IRepository)
        {
            _IRepository= IRepository;
        }

        [HttpPost]
        [Route("AddIntoShoppingCart")]
        public async Task<IActionResult> AddIntoShoppingCart(List<ShoppingCartDTO> products)
        {
            var msg = await _IRepository.AddToShoppingCart(products);
            return Ok(JsonConvert.SerializeObject(msg));
        }

        [HttpGet]
        [Route("GetShoppingCart")]

        public async Task<IActionResult> GetShoppingCart(long shoppingCartId)
        {
            var dt = await _IRepository.GetShoppingCart(shoppingCartId);
            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpPut]
        [Route("EditShoppingCart")]

        public async Task<IActionResult> EditShoppingCart(ShoppingCartCommonEditDTO objEdit)
        {
            var msg = await _IRepository.EditShoppingCart(objEdit);
            return Ok(JsonConvert.SerializeObject(msg));
        }
    }
}
