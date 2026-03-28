namespace StayFocusAPI.DTOs;

/// <summary>
/// Data transfer object for local in-memory reviews
/// </summary>
public class ReviewDto
{
    /// <summary>
    /// Unique review identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Company name
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    /// Rating (0-10)
    /// </summary>
    public float Rating { get; set; }

    /// <summary>
    /// Review title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Review content
    /// </summary>
    public string? Content { get; set; }
}
