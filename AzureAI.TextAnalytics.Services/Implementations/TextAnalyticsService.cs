using System;
using System.Threading.Tasks;
using Azure;
using AzureAI.TextAnalytics.API.Configuration;
using Microsoft.Extensions.Options;
using Azure.AI.TextAnalytics;
using AzureAI.TextAnalytics.API.Models;
using Microsoft.Extensions.Logging;

namespace AzureAI.TextAnalytics.Services.Implementations
{
    public class TextAnalyticsService : ITextAnalyticsService
    {
        private readonly ILogger<TextAnalyticsService> _logger;
        private readonly TextAnalyticsClient _client;

        public TextAnalyticsService(
            ILogger<TextAnalyticsService> logger,
            IOptions<AzureSettings> azureSettings)
        {
            _logger = logger;
            var credentials = new AzureKeyCredential(azureSettings.Value.Key);
            var endpoint = new Uri(azureSettings.Value.Endpoint);
            _client = new TextAnalyticsClient(endpoint, credentials);
        }

        public async Task<API.Models.DocumentSentiment> SentimentAnalysisAsync(string text)
        {
            try
            {
                var result = new API.Models.DocumentSentiment();
                Azure.AI.TextAnalytics.DocumentSentiment documentSentiment = _client.AnalyzeSentiment(text);
                result.Sentiment = documentSentiment.Sentiment.ToString();

                foreach (var sentence in documentSentiment.Sentences)
                {
                    var sentimentAnalysis = new SentimentAnalysis();
                    sentimentAnalysis.Text = sentence.Text;
                    sentimentAnalysis.Sentiment = sentence.Sentiment.ToString();
                    sentimentAnalysis.Positive = sentence.ConfidenceScores.Positive;
                    sentimentAnalysis.Negative = sentence.ConfidenceScores.Negative;
                    sentimentAnalysis.Neutral = sentence.ConfidenceScores.Neutral;

                    result.SentimentAnalyses.Add(sentimentAnalysis);
                }

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError($"In method ${nameof(SentimentAnalysisAsync)}: ${exception.Message}");
                return null;
            }
        }
    }
}
