//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shopping_System.DTO;
using Online_Shopping_System.Helper;
using Online_Shopping_System.IRepository;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Online_Shopping_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _IRepository;

        public ProductController(IProducts IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(List<ProductDTO> obj)
        {
            var msg = await _IRepository.CreateProduct(obj);
            return Ok(JsonConvert.SerializeObject(msg));
        }

        [HttpGet]
        [Route("GetProductLandingPagination")]
        public async Task<IActionResult> GetProductLandingPagination( string viewOrder, long pageNo, long pageSize)
        {
            var dt = await _IRepository.GetProductLandingPagination( viewOrder, pageNo, pageSize);
            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetProduct(long productId)
        {
            var dt = await _IRepository.GetProduct(productId);
            return Ok(JsonConvert.SerializeObject(dt));
        }
    }
}
