namespace StayFocusAPI.DTOs;

/// <summary>
/// AI-generated analytics and insights about the review
/// </summary>
public class AiAnalyticsDto
{
    /// <summary>
    /// Sentiment analysis score (0-1, where 1 is most positive)
    /// </summary>
    public double SentimentScore { get; set; }

    /// <summary>
    /// Sentiment label (Positive, Negative, Neutral)
    /// </summary>
    public string? SentimentLabel { get; set; }

    /// <summary>
    /// List of detected issues or concerns
    /// </summary>
    public List<string>? DetectedIssues { get; set; }

    /// <summary>
    /// Auto-generated summary of the review
    /// </summary>
    public string? AutoSummary { get; set; }

    /// <summary>
    /// Indicates if the review is flagged as spam
    /// </summary>
    public bool IsSpam { get; set; }

    /// <summary>
    /// AI trust/confidence score (0-1)
    /// </summary>
    public double AiTrustScore { get; set; }
}
