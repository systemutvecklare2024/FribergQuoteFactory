using Microsoft.AspNetCore.Mvc;

namespace FribergQuoteFactory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : Controller
    {
        [HttpGet(Name = "GetQuotes")]
        public async Task<IActionResult> Get()
        {
            return Ok("stuff");
        }
    }
}
