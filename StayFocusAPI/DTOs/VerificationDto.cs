namespace StayFocusAPI.DTOs;

/// <summary>
/// Purchase verification and authenticity information
/// </summary>
public class VerificationDto
{
    /// <summary>
    /// Indicates if this is a verified purchase
    /// </summary>
    public bool IsVerifiedPurchase { get; set; }

    /// <summary>
    /// Purchase date (ISO 8601 format)
    /// </summary>
    public string? PurchaseDate { get; set; }

    /// <summary>
    /// Usage period (e.g., "3 Months")
    /// </summary>
    public string? UsagePeriod { get; set; }

    /// <summary>
    /// Verification method used
    /// </summary>
    public string? VerificationMethod { get; set; }
}
