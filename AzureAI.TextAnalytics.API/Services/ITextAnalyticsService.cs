using System.Threading.Tasks;
using AzureAI.TextAnalytics.API.Models;

namespace AzureAI.TextAnalytics.Services
{
    public interface ITextAnalyticsService
    {
        /// <summary>
        /// Method analyses the passed text by calling Azure Text Analysis resource
        /// </summary>
        /// <param name="text">string type</param>
        /// <returns>DocumentSentiment object that will contain the sentiment label and score of the entire input document, as well as a sentiment analysis for each sentence if successful</returns>
        Task<DocumentSentiment> SentimentAnalysisAsync(string text);

        /// <summary>
        /// Method detects language in the text
        /// </summary>
        /// <param name="text">string type</param>
        /// <returns>LanguageDetection object that will contain the detected language along with its name and ISO-6391 code.</returns>
        Task<LanguageDetection> DetectLanguageAsync(string text);
    }
}
