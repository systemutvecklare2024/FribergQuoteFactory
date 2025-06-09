using FribergQuoteFactory.Api.Contracts;
using FribergQuoteFactory.Api.Data;
using FribergQuoteFactory.Api.Models;
using FribergQuoteFactory.Api.Repositories;
using FribergQuoteFactory.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace FribergQuoteFactory.Tests.Systems.Repositories
{
    public class QuoteRepositoryTests : IDisposable
    {
        private readonly QuoteDbContext dbContext;

        public QuoteRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<QuoteDbContext>().UseInMemoryDatabase("test").Options;
            dbContext = new QuoteDbContext(contextOptions);
            dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddQuote_WithValidQuote_AddsQuoteToRepository()
        {
            // Arrange
            var quoteRepository = new QuoteRepository(dbContext);

            var newQuote = new Quote
            {
                QuoteText = "Carpe Diem",
                Category = "motivation",
            };

            // Act
            await quoteRepository.AddAsync(newQuote);

            // Assert
            Assert.Contains(newQuote, await quoteRepository.GetAllAsync());
        }

        [Fact]
        public async Task AddRange_AddsQuotesToRepository()
        {
            // Arrange
            var quoteRepository = new QuoteRepository(dbContext);

            // Act
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetSingleQuote());

            // Assert
            var allQuotes = await quoteRepository.GetAllAsync();
            var single = Assert.Single(allQuotes);
            Assert.Equal("Carpe Diem", single.QuoteText);
        }

        [Fact]
        public async Task Random_WithListOfQuotes_ReturnsRandomQuote()
        {
            // Arrange
            var quoteRepository = new QuoteRepository(dbContext);
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotes());

            // Act
            var quote = await quoteRepository.GetRandomQuoteAsync();

            // Assert
            Assert.NotNull(quote);
            Assert.Contains(quote, await quoteRepository.GetAllAsync());
        }

        [Fact]
        public async Task Random_WithListOfQuotesAndGivenCategory_ReturnsRandomQuoteOfCategory()
        {
            // Arrange
            var quoteRepository = new QuoteRepository(dbContext);
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotes());
            var category = "motivation";

            // Act
            var quote = await quoteRepository.GetRandomQuoteAsync(category);

            // Assert
            Assert.NotNull(quote);
            Assert.Equal(category, quote.Category);
            Assert.Contains(quote, await quoteRepository.GetAllAsync());

        }

        [Fact]
        public async Task Random_WithLimitedQuotesAndGivenCategory_AlwaysReturnsApprovedQuote()
        {
            // Arrange
            var quoteRepository = new QuoteRepository(dbContext);
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotesForApproveTest());
            var category = "entrepreneurship";
            var amountOfTries = 20;
            List<Quote> quotes = [];

            // Act
            for(int i = 0;i<amountOfTries;i++)
            {
                var quote = await quoteRepository.GetRandomQuoteAsync(category);
                quotes.Add(quote);
            }

            // Assert
            Assert.All(quotes, q => Assert.NotNull(q));
            Assert.All(quotes, q => Assert.True(q.Approved));
            Assert.All(quotes, q => Assert.Equal(category, q.Category.ToString()));
            Assert.All(quotes, q => Assert.Equal("Fortune favors the bold.", q.QuoteText));
        }

        [Fact]
        public async Task Random_WithListOfQuotesAndWrongCategory_ThrowsException()
        {
            // Arrange
            var quoteRepository = new QuoteRepository(dbContext);
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotes());
            var category = "test";

            // Act && Assert
            _ = Assert.ThrowsAsync<InvalidOperationException>(async () => await quoteRepository.GetRandomQuoteAsync(category));
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
        }
    }
}
