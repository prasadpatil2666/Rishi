namespace StayFocusAPI.DTOs;

/// <summary>
/// Social media feed integration information
/// </summary>
public class SocialFeedsDto
{
    /// <summary>
    /// Instagram post information
    /// </summary>
    public InstagramDto? Instagram { get; set; }

    /// <summary>
    /// YouTube video information
    /// </summary>
    public YouTubeDto? YouTube { get; set; }

    /// <summary>
    /// Twitter/X post information
    /// </summary>
    public TwitterDto? Twitter { get; set; }
}

/// <summary>
/// Instagram post details
/// </summary>
public class InstagramDto
{
    /// <summary>
    /// Instagram post ID
    /// </summary>
    public string? PostId { get; set; }

    /// <summary>
    /// Instagram post embed URL
    /// </summary>
    public string? EmbedUrl { get; set; }

    /// <summary>
    /// Sync status with Instagram
    /// </summary>
    public string? SyncStatus { get; set; }
}

/// <summary>
/// YouTube video details
/// </summary>
public class YouTubeDto
{
    /// <summary>
    /// YouTube video ID
    /// </summary>
    public string? VideoId { get; set; }

    /// <summary>
    /// Full YouTube video URL
    /// </summary>
    public string? VideoUrl { get; set; }
}

/// <summary>
/// Twitter/X post details
/// </summary>
public class TwitterDto
{
    /// <summary>
    /// Tweet ID
    /// </summary>
    public string? TweetId { get; set; }

    /// <summary>
    /// Tweet URL
    /// </summary>
    public string? Url { get; set; }
}
