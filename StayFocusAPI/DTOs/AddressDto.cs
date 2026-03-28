namespace StayFocusAPI.DTOs;

/// <summary>
/// Physical address information
/// </summary>
public class AddressDto
{
    /// <summary>
    /// Area or neighborhood
    /// </summary>
    public string? Area { get; set; }

    /// <summary>
    /// Postal/ZIP code
    /// </summary>
    public string? ZipCode { get; set; }

    /// <summary>
    /// Landmark or reference point
    /// </summary>
    public string? Landmark { get; set; }
}
