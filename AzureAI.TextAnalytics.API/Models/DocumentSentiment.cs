using System.Collections.Generic;

namespace AzureAI.TextAnalytics.API.Models
{
    public class DocumentSentiment
    {
        /// <summary>
        /// Document sentiment
        /// </summary>
        public string Sentiment { get; set; }

        /// <summary>
        /// Sentences analyses
        /// </summary>
        public List<SentimentAnalysis> SentimentAnalyses { get; set; }

        public DocumentSentiment()
        {
            SentimentAnalyses = new List<SentimentAnalysis>();
        }
    }
}
