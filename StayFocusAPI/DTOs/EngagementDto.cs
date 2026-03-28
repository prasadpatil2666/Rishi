namespace StayFocusAPI.DTOs;

/// <summary>
/// User engagement and interaction metrics
/// </summary>
public class EngagementDto
{
    /// <summary>
    /// Total number of views
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Number of helpful votes
    /// </summary>
    public int HelpfulCount { get; set; }

    /// <summary>
    /// Number of abuse reports
    /// </summary>
    public int ReportCount { get; set; }

    /// <summary>
    /// Share count breakdown by platform
    /// </summary>
    public ShareCountDto? ShareCount { get; set; }
}

/// <summary>
/// Share count breakdown by social media platform
/// </summary>
public class ShareCountDto
{
    /// <summary>
    /// Number of WhatsApp shares
    /// </summary>
    public int WhatsApp { get; set; }

    /// <summary>
    /// Number of Facebook shares
    /// </summary>
    public int Facebook { get; set; }

    /// <summary>
    /// Number of Twitter/X shares
    /// </summary>
    public int Twitter { get; set; }

    /// <summary>
    /// Number of direct link shares
    /// </summary>
    public int DirectLink { get; set; }
}
