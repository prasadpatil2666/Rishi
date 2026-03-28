namespace StayFocusAPI.DTOs;

/// <summary>
/// Core review content, ratings, and verification information
/// </summary>
public class ReviewDataDto
{
    /// <summary>
    /// Overall rating (0-10 scale)
    /// </summary>
    public double Rating { get; set; }

    /// <summary>
    /// Review title/headline
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Detailed review comment/description
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// List of positive aspects (pros)
    /// </summary>
    public List<string>? Pros { get; set; }

    /// <summary>
    /// List of negative aspects (cons)
    /// </summary>
    public List<string>? Cons { get; set; }

    /// <summary>
    /// Multi-criteria rating breakdown
    /// </summary>
    public DetailedRatingsDto? DetailedRatings { get; set; }

    /// <summary>
    /// Purchase verification information
    /// </summary>
    public VerificationDto? Verification { get; set; }
}
