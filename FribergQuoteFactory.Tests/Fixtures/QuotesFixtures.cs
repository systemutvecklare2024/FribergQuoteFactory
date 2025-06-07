using FribergQuoteFactory.Api.Models;

namespace FribergQuoteFactory.Tests.Fixtures
{
    public class QuotesFixtures
    {
        public static List<Quote> GetSingleQuote() => new()
        {
            new Quote
            {
                QuoteText = "Carpe Diem",
                Category = "motivation"
            }
        };

        public static List<Quote> GetQuotes() => new()
        {
            new Quote
            {
                QuoteText = "Fortune favors the bold.",
                Category = "entrepreneurship",
                Approved = true
            },
            new Quote
            {
                QuoteText = "Start where you are. Use what you have. Do what you can.",
                Category = "entrepreneurship",
                Approved = true
            },
            new Quote
            {
                QuoteText = "Opportunities don't happen. You create them.",
                Category = "entrepreneurship"
            },
            new Quote
            {
                QuoteText = "Know thyself.",
                Category = "self-development",
                Approved = true
            },
            new Quote
            {
                QuoteText = "Be the change you wish to see in the world.",
                Category = "self-development"
            },
            new Quote
            {
                QuoteText = "Progress, not perfection.",
                Category = "self-development",
                Approved = true
            },
            new Quote
            {
                QuoteText = "Carpe diem.",
                Category = "motivation"
            },
            new Quote
            {
                QuoteText = "The best way out is always through.",
                Category = "motivation",
                Approved = true
            },
            new Quote
            {
                QuoteText = "Stay hungry. Stay foolish.",
                Category = "motivation"
            },
            new Quote
            {
                QuoteText = "Lead by example.",
                Category = "leadership",
                Approved = true
            },
            new Quote
            {
                QuoteText = "A leader is one who knows the way, goes the way, and shows the way.",
                Category = "leadership"
            },
            new Quote
            {
                QuoteText = "The greatest leader is not necessarily the one who does the greatest things. He is the one that gets the people to do the greatest things.",
                Category = "leadership"
            },
            new Quote
            {
                QuoteText = "Success is not final, failure is not fatal: it is the courage to continue that counts.",
                Category = "success"
            },
            new Quote
            {
                QuoteText = "uccess usually comes to those who are too busy to be looking for it.",
                Category = "success",
                Approved = true
            },
            new Quote
            {
                QuoteText = "Dream big. Work hard. Stay focused.",
                Category = "success"
            }
        };
    }
}
