using FribergQuoteFactory.Api.Models;

namespace FribergQuoteFactory.Api.Contracts
{
    public interface IQuoteRepository
    {
        Task<Quote> AddAsync(Quote newQuote);
        Task AddRangeAsync(IEnumerable<Quote> quotes);
        Task ApproveQuoteAsync(Guid id);
        Task<IEnumerable<Quote>> GetAllAsync();
        Task<Quote?> GetAsync(Guid guid);
        Task<Quote> GetRandomQuoteAsync(string category = "");
        Task<IEnumerable<Quote>> GetUnapprovedQuotesAsync();
    }
}
