namespace StayFocusAPI.DTOs;

/// <summary>
/// Geographic location information with coordinates
/// </summary>
public class LocationDto
{
    /// <summary>
    /// GeoJSON type (typically "Point")
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Geographic coordinates [longitude, latitude]
    /// </summary>
    public List<double>? Coordinates { get; set; }
}
