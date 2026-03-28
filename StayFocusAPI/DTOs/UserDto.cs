namespace StayFocusAPI.DTOs;

/// <summary>
/// Review author/user information
/// </summary>
public class UserDto
{
    /// <summary>
    /// Unique user identifier
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Display name of the user
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Indicates if user is an expert reviewer
    /// </summary>
    public bool IsExpert { get; set; }

    /// <summary>
    /// URL to user's profile picture
    /// </summary>
    public string? ProfilePicture { get; set; }
}
