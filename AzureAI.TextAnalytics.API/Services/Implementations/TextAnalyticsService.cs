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
        private readonly TextAnalyticsClient _client;
        private readonly ILogger<TextAnalyticsService> _logger;

        public TextAnalyticsService(
            ILogger<TextAnalyticsService> logger,
            IOptions<AzureSettings> azureSettings)
        {
            var credentials = new AzureKeyCredential(azureSettings.Value.Key);
            var endpoint = new Uri(azureSettings.Value.Endpoint);
            _client = new TextAnalyticsClient(endpoint, credentials);
            _logger = logger;
        }

        /// <summary>
        /// Method analyses the passed text by calling Azure Text Analysis resource
        /// </summary>
        /// <param name="text">string type</param>
        /// <returns>DocumentSentiment object that will contain the sentiment label and score of the entire input document, as well as a sentiment analysis for each sentence if successful</returns>
        public async Task<API.Models.DocumentSentiment> SentimentAnalysisAsync(string text)
        {
            try
            {
                var result = new API.Models.DocumentSentiment();
                Azure.AI.TextAnalytics.DocumentSentiment documentSentiment = await _client.AnalyzeSentimentAsync(text);
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

        /// <summary>
        /// Method detects language in the text
        /// </summary>
        /// <param name="text">string type</param>
        /// <returns>LanguageDetection object that will contain the detected language along with its name and ISO-6391 code.</returns>
        public async Task<LanguageDetection> DetectLanguageAsync(string text)
        {
            try
            {
                DetectedLanguage detectedLanguage = await _client.DetectLanguageAsync(text);
                return new LanguageDetection()
                {
                    Language = detectedLanguage.Name,
                    Standard = detectedLanguage.Iso6391Name
                };
            }
            catch (Exception exception)
            {
                _logger.LogError($"In method ${nameof(LanguageDetection)}: ${exception.Message}");
                return null;
            }
        }
    }
}
