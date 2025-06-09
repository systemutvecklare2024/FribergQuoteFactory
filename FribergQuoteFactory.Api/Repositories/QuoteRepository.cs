using FribergQuoteFactory.Api.Contracts;
using FribergQuoteFactory.Api.Data;
using FribergQuoteFactory.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergQuoteFactory.Api.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly QuoteDbContext dbContext;

        public QuoteRepository(QuoteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(Quote newQuote)
        {
            await dbContext.Quotes.AddAsync(newQuote);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Quote> quotes)
        {
            await dbContext.Quotes.AddRangeAsync(quotes);
            await dbContext.SaveChangesAsync();
        }

        public async Task ApproveQuoteAsync(Guid id)
        {
            var quote = await dbContext.Quotes.FirstOrDefaultAsync(q => q.Id == id);
            if (quote == null)
            {
                throw new InvalidOperationException("Quote not found.");
            }

            quote.Approved = true;
            dbContext.Quotes.Update(quote);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Quote>> GetAllAsync()
        {
            return await dbContext.Quotes.ToListAsync();
        }

        public async Task<Quote> GetRandomQuoteAsync(string category = "")
        {
            var allQuotes = await dbContext.Quotes
                .Where(q => q.Approved)
                .ToListAsync();

            var random = new Random();

            List<Quote> sourceQuotes;

            if (string.IsNullOrEmpty(category))
            {
                sourceQuotes = allQuotes;
            }
            else
            {
                sourceQuotes = allQuotes
                    .Where(q => q.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if(!sourceQuotes.Any())
            {
                throw new InvalidOperationException("No quotes found for the given category");
            }

            return sourceQuotes[random.Next(sourceQuotes.Count)];
        }

        public async Task<IEnumerable<Quote>> GetUnapprovedQuotesAsync()
        {
            return await dbContext.Quotes.Where(q => !q.Approved).ToListAsync();
        }
    }
}
