using System.Threading.Tasks;
using AzureAI.TextAnalytics.API.Models;

namespace AzureAI.TextAnalytics.Services
{
    public interface ITextAnalyticsService
    {
        Task<DocumentSentiment> SentimentAnalysisAsync(string text);
    }
}
