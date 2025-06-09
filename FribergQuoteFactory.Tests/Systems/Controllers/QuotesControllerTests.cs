
using FribergQuoteFactory.Api.Data;
using FribergQuoteFactory.Api.Dtos;
using FribergQuoteFactory.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace FribergQuoteFactory.Tests.Systems.Controllers
{
    public class QuotesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;
        private readonly HttpClient httpClient;

        public QuotesControllerTests(WebApplicationFactory<Program> factory)
        {
            // Replace the default database with InMemoryDB
            _applicationFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<QuoteDbContext>));
                    
                    if(descriptor != null) 
                        services.Remove(descriptor);

                    services.AddDbContext<QuoteDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("test");
                    });
                });
            });

            httpClient = _applicationFactory.CreateClient();
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
            Assert.Equal(newQuote.QuoteText, result.QuoteText);
            Assert.Equal(newQuote.Category, result.Category);
        }
    }
}
