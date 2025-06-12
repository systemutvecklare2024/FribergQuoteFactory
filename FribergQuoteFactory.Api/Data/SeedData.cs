using FribergQuoteFactory.Api.Models;

namespace FribergQuoteFactory.Api.Data
{
    public static class SeedData
    {
        public static List<Quote> SeedQuotes = new List<Quote>
{
    // Entrepreneurship (1-10)
    new Quote { Id = Guid.NewGuid(), QuoteText = "The best way to predict the future is to create it.", Category = "entrepreneurship", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Ideas are easy. Implementation is hard.", Category = "entrepreneurship", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Don't be afraid to give up the good to go for the great.", Category = "entrepreneurship", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Your most unhappy customers are your greatest source of learning.", Category = "entrepreneurship", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The secret of getting ahead is getting started.", Category = "entrepreneurship", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "If you're not embarrassed by the first version of your product, you've launched too late.", Category = "entrepreneurship", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The biggest risk is not taking any risk.", Category = "entrepreneurship", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Entrepreneurship is living a few years of your life like most people won't, so you can spend the rest of your life like most people can't.", Category = "entrepreneurship", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Success is walking from failure to failure with no loss of enthusiasm.", Category = "entrepreneurship", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The only limit to our realization of tomorrow is our doubts of today.", Category = "entrepreneurship", Approved = false },

    // Self-development (11-20)
    new Quote { Id = Guid.NewGuid(), QuoteText = "The only person you are destined to become is the person you decide to be.", Category = "self-development", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "An investment in knowledge pays the best interest.", Category = "self-development", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "You are the average of the five people you spend the most time with.", Category = "self-development", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The mind is everything. What you think you become.", Category = "self-development", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The first and greatest victory is to conquer yourself.", Category = "self-development", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Your life does not get better by chance, it gets better by change.", Category = "self-development", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The only way to do great work is to love what you do.", Category = "self-development", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "We are what we repeatedly do. Excellence, then, is not an act, but a habit.", Category = "self-development", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The expert in anything was once a beginner.", Category = "self-development", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Personal development is the belief that you are worth the effort, time, and energy needed to develop yourself.", Category = "self-development", Approved = true },

    // Motivation (21-30)
    new Quote { Id = Guid.NewGuid(), QuoteText = "Believe you can and you're halfway there.", Category = "motivation", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "It always seems impossible until it's done.", Category = "motivation", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Don't watch the clock; do what it does. Keep going.", Category = "motivation", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The only place where success comes before work is in the dictionary.", Category = "motivation", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "You don't have to be great to start, but you have to start to be great.", Category = "motivation", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Push yourself, because no one else is going to do it for you.", Category = "motivation", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The pain you feel today will be the strength you feel tomorrow.", Category = "motivation", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Dream bigger. Do bigger.", Category = "motivation", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Success is the sum of small efforts, repeated day in and day out.", Category = "motivation", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The harder you work for something, the greater you'll feel when you achieve it.", Category = "motivation", Approved = false },

    // Leadership (31-40)
    new Quote { Id = Guid.NewGuid(), QuoteText = "A leader is one who knows the way, goes the way, and shows the way.", Category = "leadership", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Leadership is the capacity to translate vision into reality.", Category = "leadership", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The greatest leader is not necessarily the one who does the greatest things. He is the one that gets the people to do the greatest things.", Category = "leadership", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Before you are a leader, success is all about growing yourself. When you become a leader, success is all about growing others.", Category = "leadership", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Leadership is not about being in charge. It's about taking care of those in your charge.", Category = "leadership", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "A good leader takes a little more than his share of the blame, a little less than his share of the credit.", Category = "leadership", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The art of leadership is saying no, not yes. It is very easy to say yes.", Category = "leadership", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Leadership is the ability to guide others without force into a direction or decision that leaves them still feeling empowered and accomplished.", Category = "leadership", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The function of leadership is to produce more leaders, not more followers.", Category = "leadership", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Leadership is not about titles, positions or flowcharts. It is about one life influencing another.", Category = "leadership", Approved = true },

    // Success (41-50)
     new Quote { Id = Guid.NewGuid(), QuoteText = "Success is not final, failure is not fatal: It is the courage to continue that counts.", Category = "success", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Success usually comes to those who are too busy to be looking for it.", Category = "success", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The road to success and the road to failure are almost exactly the same.", Category = "success", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Success isn't about how much money you make, it's about the difference you make in people's lives.", Category = "success", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Success is getting what you want. Happiness is wanting what you get.", Category = "success", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The only place where success comes before work is in the dictionary.", Category = "success", Approved = false },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Success is the sum of small efforts, repeated day in and day out.", Category = "success", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Success is not the key to happiness. Happiness is the key to success.", Category = "success", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "Success is not in what you have, but who you are.", Category = "success", Approved = true },
    new Quote { Id = Guid.NewGuid(), QuoteText = "The secret of success is to do the common thing uncommonly well.", Category = "success", Approved = false }
};
    }


}
