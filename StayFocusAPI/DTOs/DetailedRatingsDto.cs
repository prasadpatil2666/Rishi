namespace StayFocusAPI.DTOs;

/// <summary>
/// Multi-criteria rating breakdown for comprehensive review analysis
/// </summary>
public class DetailedRatingsDto
{
    /// <summary>
    /// Value for money rating (0-10)
    /// </summary>
    public double ValueForMoney { get; set; }

    /// <summary>
    /// Build quality rating (0-10)
    /// </summary>
    public double BuildQuality { get; set; }

    /// <summary>
    /// Customer service rating (0-10)
    /// </summary>
    public double CustomerService { get; set; }

    /// <summary>
    /// Delivery experience rating (0-10)
    /// </summary>
    public double DeliveryExperience { get; set; }

    /// <summary>
    /// Reliability rating (0-10)
    /// </summary>
    public double Reliability { get; set; }

    /// <summary>
    /// Performance rating (0-10)
    /// </summary>
    public double Performance { get; set; }
}
