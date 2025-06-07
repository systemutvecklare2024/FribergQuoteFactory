using FribergQuoteFactory.Api.Models;

namespace FribergQuoteFactory.Api.Contracts
{
    public interface IQuoteRepository
    {
        Task AddAsync(Quote newQuote);
        Task AddRangeAsync(IEnumerable<Quote> quotes);
    }
}
