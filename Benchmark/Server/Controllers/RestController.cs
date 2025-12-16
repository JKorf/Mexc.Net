using Microsoft.AspNetCore.Mvc;

namespace Binance.Net.Benchmark.Controllers
{
    [ApiController]
    [Route("api/v3")]
    public class RestController : ControllerBase
    {
        [HttpGet("time")]
        public async Task<object> Get()
        {
            Response.ContentType = "application/json";
            var response = new { serverTime = 1763802578 };
            return response;            
        }
    }
}
