namespace StayFocusAPI.DTOs;

/// <summary>
/// Company information and branding details
/// </summary>
public class CompanyDetailsDto
{
    /// <summary>
    /// Company identifier
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Company display name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Global brand identifier
    /// </summary>
    public string? GlobalBrandId { get; set; }

    /// <summary>
    /// Indicates if the brand is verified
    /// </summary>
    public bool IsVerifiedBrand { get; set; }

    /// <summary>
    /// URL to company/brand logo
    /// </summary>
    public string? BrandLogoUrl { get; set; }
}
