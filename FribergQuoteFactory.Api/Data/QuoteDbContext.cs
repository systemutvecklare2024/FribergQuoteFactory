using FribergQuoteFactory.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergQuoteFactory.Api.Data
{
    public class QuoteDbContext : DbContext
    {

        public DbSet<Quote> Quotes { get; set; }
        public QuoteDbContext(DbContextOptions<QuoteDbContext> options) : base(options) {}



    }
}
