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

                    services.AddDbContext<QuoteDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("test");
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

            // GET quotes/{category}/randomquote
            // GET quotes/{guid} <- get by id
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
            Assert.Empty(result);
        }


        [Fact]
        public async Task AapproveQuote_WithGuid_ReturnsOk()
        {
            using (var scope = applicationFactory.Services.CreateScope())
            {
                // Arrange
                
                var repo = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();
                await repo.AddRangeAsync(QuotesFixtures.GetQuotes());
                
                var quoteToApprove = (await repo.GetUnapprovedQuotesAsync()).FirstOrDefault() ?? throw new InvalidOperationException("No unapproved quotes found.")


                //// Act
                //var response = await httpClient.PutAsync($"quotes/{quoteToApprove.Id}/Approve");

                //// Assert
                
                //var refreshedQuote await repo.GetAllAsync();

                //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                //Assert.
                //Assert.Empty(result);
            }
        }
    }
}
