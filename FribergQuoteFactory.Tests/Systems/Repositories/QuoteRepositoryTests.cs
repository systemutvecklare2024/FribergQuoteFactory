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
        private readonly IQuoteRepository quoteRepository;

        public QuoteRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<QuoteDbContext>()
                .UseInMemoryDatabase("test")
                .Options;

            dbContext = new QuoteDbContext(contextOptions);
            dbContext.Database.EnsureCreated();

            quoteRepository = new QuoteRepository(dbContext);
        }

        [Fact]
        public async Task AddAsync_WithValidQuote_AddsQuoteToRepository()
        {
            // Arrange
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
        public async Task AddAsync_WithValidQuote_AddedQuoteIsReturned()
        {
            // Arrange
            var newQuote = new Quote
            {
                QuoteText = "Carpe Diem",
                Category = "motivation",
            };

            // Act
            var result = await quoteRepository.AddAsync(newQuote);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(newQuote, await quoteRepository.GetAllAsync());
        }

        [Fact]
        public async Task AddRangeAsync_AddsQuotesToRepository()
        {
            // Arrange

            // Act
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetSingleQuote());

            // Assert
            var allQuotes = await quoteRepository.GetAllAsync();
            var single = Assert.Single(allQuotes);
            Assert.Equal("Carpe Diem", single.QuoteText);
        }

        [Fact]
        public async Task GetRandomQuoteAsync_WithListOfQuotes_ReturnsRandomQuote()
        {
            // Arrange
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotes());

            // Act
            var quote = await quoteRepository.GetRandomQuoteAsync();

            // Assert
            Assert.NotNull(quote);
            Assert.Contains(quote, await quoteRepository.GetAllAsync());
        }

        [Fact]
        public async Task GetRandomQuoteAsync_WithListOfQuotesAndGivenCategory_ReturnsRandomQuoteOfCategory()
        {
            // Arrange
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
        public async Task GetRandomQuoteAsync_WithLimitedQuotesAndGivenCategory_AlwaysReturnsApprovedQuote()
        {
            // Arrange
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotesForApproveTest());
            var category = "entrepreneurship";
            var amountOfTries = 20;
            List<Quote> quotes = [];

            // Act
            for (int i = 0; i < amountOfTries; i++)
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
        public async Task GetRandomQuoteAsync_WithListOfQuotesAndWrongCategory_ThrowsException()
        {
            // Arrange
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotes());
            var category = "test";

            // Act && Assert
            _ = Assert.ThrowsAsync<InvalidOperationException>(async () => await quoteRepository.GetRandomQuoteAsync(category));
        }

        [Fact]
        public async Task GetUnapprovedAsync_WithListOfQuotes_ReturnsListOfUnapprovedQuotes()
        {
            // Arrange
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotes());

            // Act
            var quotes = await quoteRepository.GetUnapprovedQuotesAsync();

            // Assert
            Assert.All(quotes, q => Assert.False(q.Approved));
        }

        [Fact]
        public async Task GetUnapprovedAsync_WithNoUnapprovedQuotes_ReturnsEmptyArray()
        {
            // Arrange
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetOnlyApprovedQuotes());

            // Act
            var quotes = await quoteRepository.GetUnapprovedQuotesAsync();

            // Assert
            Assert.Empty(quotes);
        }

        [Fact]
        public async Task ApproveQuoteAsync_WithValidId_SetsQuoteAsApproved()
        {
            // Arrange
            var quote = new Quote { Id = Guid.NewGuid(), QuoteText = "Carpe Diem.", Category = "motivation" };
            await quoteRepository.AddAsync(quote);
            var quoteFromRepo = (await quoteRepository.GetAllAsync()).FirstOrDefault();

            // Act
            await quoteRepository.ApproveQuoteAsync(quoteFromRepo.Id);
            var updatedQuote = (await quoteRepository.GetAllAsync()).FirstOrDefault();

            // Assert
            Assert.True(updatedQuote?.Approved);
        }

        [Fact]
        public async Task ApproveQuoteAsync_WithNoValidId_ThrowsException()
        {
            // Arrange
            await quoteRepository.AddRangeAsync(QuotesFixtures.GetQuotes());

            // Act
            // Assert
            _ = Assert.ThrowsAsync<InvalidOperationException>(async () => await quoteRepository.ApproveQuoteAsync(Guid.NewGuid()));
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
        }
    }
}
