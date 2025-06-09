using System.ComponentModel.DataAnnotations;

namespace FribergQuoteFactory.Api.Dtos
{
    public class CreateQuoteDto
    {
        [Required]
        public string QuoteText { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
