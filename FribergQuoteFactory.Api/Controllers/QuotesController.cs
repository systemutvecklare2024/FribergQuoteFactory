using FribergQuoteFactory.Api.Contracts;
using FribergQuoteFactory.Api.Dtos;
using FribergQuoteFactory.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FribergQuoteFactory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly IQuoteRepository repository;

        public QuotesController(IQuoteRepository repository)
        {
            this.repository = repository;
        }


        [HttpPost(Name = "CreateQuote")]
        public async Task<IActionResult> Create([FromBody] CreateQuoteDto createQuoteDto)
        {
            if (createQuoteDto == null)
                return BadRequest("Invalid data");

            var quote = new Quote
            {
                QuoteText = createQuoteDto.QuoteText,
                Category = createQuoteDto.Category,
            };

            var res = await repository.AddAsync(quote);
            if (res == null)
                return BadRequest("Unable to create quote");

            return Created($"quotes/{res.Id}", res);
        }
    }
}
