using FribergQuoteFactory.Api.Contracts;
using FribergQuoteFactory.Api.Data;
using FribergQuoteFactory.Api.Dtos;
using FribergQuoteFactory.Api.Models;
using FribergQuoteFactory.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace FribergQuoteFactory.Tests.Systems.Controllers
{
    public class QuotesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> applicationFactory;
        private readonly HttpClient httpClient;

        public QuotesControllerTests(WebApplicationFactory<Program> factory)
        {
            // Replace the default database with InMemoryDB
            applicationFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<QuoteDbContext>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    var dbName = Guid.NewGuid().ToString();

                    services.AddDbContext<QuoteDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(dbName);
                    });
                });
            });

            httpClient = applicationFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7118/api/");
        }

        [Fact]
        public async Task PostQuote_WithValidQuoteDto_CreatesAndReturnsNewQuoteWithCreatedStatusCode()
        {
            // Arrange
            var newQuote = new CreateQuoteDto
            {
                QuoteText = "Carpe diem",
                Category = "motivation"
            };

            // Act
            var response = await httpClient.PostAsJsonAsync("quotes", newQuote);
            var result = await response.Content.ReadFromJsonAsync<Quote>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(newQuote.QuoteText, result?.QuoteText);
            Assert.Equal(newQuote.Category, result?.Category);
        }

        [Fact]
        public async Task PostQuote_WithMissingFields_Returns400()
        {
            // Arrange
            var newQuote = new CreateQuoteDto
            {
                QuoteText = "Carpe diem"
            };

            // Act
            var response = await httpClient.PostAsJsonAsync("quotes", newQuote);
            var result = await response.Content.ReadFromJsonAsync<Quote>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetRandomQuote_WithQuotes_ReturnsQuote()
        {
            // Arrange
            //Seed
            using (var scope = applicationFactory.Services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();
                await repo.AddRangeAsync(QuotesFixtures.GetQuotes());
            }

            // Act
            var response = await httpClient.GetAsync("quotes/random");
            var result = await response.Content.ReadFromJsonAsync<Quote>();


            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetRandomQuote_WithQuotesAndSpecifiedCategory_ReturnsQuoteOfCategory()
        {
            // Arrange
            //Seed
            using (var scope = applicationFactory.Services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();
                await repo.AddRangeAsync(QuotesFixtures.GetQuotes());
            }
            var category = "motivation";

            // Act
            var response = await httpClient.GetAsync($"quotes/random?category={category}");
            var result = await response.Content.ReadFromJsonAsync<Quote>();


            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(category, result.Category);
        }

        [Fact]
        public async Task GetRandomQuote_WithNoQuotes_ReturnsNotFound()
        {
            var response = await httpClient.GetAsync($"quotes/random");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetRandomQuote_WithNonExistentCategory_ReturnsNotFound()
        {
            // Arrange
            //Seed
            using (var scope = applicationFactory.Services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();
                await repo.AddRangeAsync(QuotesFixtures.GetQuotes());
            }
            var category = "non-existent";

            // Act
            var response = await httpClient.GetAsync($"quotes/random?category={category}");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task GetUnapproved_WithQuotes_ReturnsAllUnapprovedQuotes()
        {
            // Arrange
            //Seed
            using (var scope = applicationFactory.Services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();
                await repo.AddRangeAsync(QuotesFixtures.GetQuotes());
            }

            // Act
            var response = await httpClient.GetAsync($"quotes/unapproved");
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Quote>>();


            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.All(result, q => Assert.False(q.Approved));
        }

        [Fact]
        public async Task GetUnapproved_WithoutQuotes_ReturnsEmpty()
        {
            // Act
            var response = await httpClient.GetAsync($"quotes/unapproved");
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Quote>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Empty(result);
        }


        [Fact]
        public async Task ApproveQuote_WithGuid_ReturnsOk()
        {
            Guid quoteId;

            // Test Approving
            using (var scope = applicationFactory.Services.CreateScope())
            {
                // Arrange

                var repo = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();
                await repo.AddRangeAsync(QuotesFixtures.GetQuotes());

                var quoteToApprove = (await repo.GetUnapprovedQuotesAsync()).FirstOrDefault() ?? throw new InvalidOperationException("No unapproved quotes found.");
                quoteId = quoteToApprove.Id;

                // Act
                var response = await httpClient.PutAsync($"quotes/{quoteId}/Approve", null);

                //// Assert
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            }

            // Validate it got approved
            using (var scope2 = applicationFactory.Services.CreateScope())
            {
                // Arrange
                var repo = scope2.ServiceProvider.GetRequiredService<IQuoteRepository>();

                // Act
                var quote = await repo.GetAsync(quoteId);

                // Assert
                Assert.NotNull(quote);
                Assert.True(quote.Approved);
            }
        }

        [Theory]
        [InlineData("74078bb5-666d-4948-a7af-039103cd41f6", HttpStatusCode.NotFound)]
        [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.BadRequest)]
        public async Task ApproveQuote_WithInvalidGuid_Returns(Guid id, HttpStatusCode expectedCode)
        {
            // Test Approving
            using (var scope = applicationFactory.Services.CreateScope())
            {
                // Arrange
                var repo = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();

                // Act
                var response = await httpClient.PutAsync($"quotes/{id}/Approve", null);

                //// Assert
                Assert.Equal(expectedCode, response.StatusCode);
            }
        }
    }
}
