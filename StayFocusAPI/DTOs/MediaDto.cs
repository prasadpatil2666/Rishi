namespace StayFocusAPI.DTOs;

/// <summary>
/// Media attachment information (videos, images, audio files)
/// </summary>
public class MediaDto
{
    /// <summary>
    /// Type of media (video, image, audio)
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// URL to the media file
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Thumbnail image URL (for videos)
    /// </summary>
    public string? Thumbnail { get; set; }

    /// <summary>
    /// Duration in seconds (for audio/video)
    /// </summary>
    public int? DurationSeconds { get; set; }

    /// <summary>
    /// Aspect ratio (e.g., "9:16")
    /// </summary>
    public string? AspectRatio { get; set; }

    /// <summary>
    /// Alternative text (for images)
    /// </summary>
    public string? AltText { get; set; }

    /// <summary>
    /// Label or description (for audio)
    /// </summary>
    public string? Label { get; set; }
}
