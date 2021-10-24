namespace AzureAI.TextAnalytics.API.Models
{
    public class SentimentAnalysis
    {
        /// <summary>
        /// Sentence text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Sentiment (Positive, Negative or Neutral)
        /// </summary>
        public string Sentiment { get; set; }

        /// <summary>
        /// Positive score
        /// </summary>
        public double Positive { get; set; }

        /// <summary>
        /// Negative score
        /// </summary>
        public double Negative { get; set; }

        /// <summary>
        /// Neutral score
        /// </summary>
        public double Neutral { get; set; }
    }
}
