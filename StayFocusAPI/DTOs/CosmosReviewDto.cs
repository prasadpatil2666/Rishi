namespace StayFocusAPI.DTOs;

/// <summary>
/// Main review data transfer object for Azure Cosmos DB reviews
/// </summary>
public class CosmosReviewDto
{
    /// <summary>
    /// Unique review identifier
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Cosmos DB partition key
    /// </summary>
    public string? PartitionKey { get; set; }

    /// <summary>
    /// Data schema version
    /// </summary>
    public string? SchemaVersion { get; set; }

    /// <summary>
    /// ISO country code
    /// </summary>
    public string? CountryCode { get; set; }

    /// <summary>
    /// State and city identifier
    /// </summary>
    public string? StateCity { get; set; }

    /// <summary>
    /// Product/service category
    /// </summary>
    public string? CategoryId { get; set; }

    /// <summary>
    /// Geographic location details
    /// </summary>
    public LocationDto? Location { get; set; }

    /// <summary>
    /// Physical address details
    /// </summary>
    public AddressDto? Address { get; set; }

    /// <summary>
    /// Company information
    /// </summary>
    public CompanyDetailsDto? CompanyDetails { get; set; }

    /// <summary>
    /// Review content and ratings
    /// </summary>
    public ReviewDataDto? ReviewData { get; set; }

    /// <summary>
    /// Media attachments (videos, images, audio)
    /// </summary>
    public List<MediaDto>? Media { get; set; }

    /// <summary>
    /// Social media feed integrations
    /// </summary>
    public SocialFeedsDto? SocialFeeds { get; set; }

    /// <summary>
    /// Engagement metrics
    /// </summary>
    public EngagementDto? Engagement { get; set; }

    /// <summary>
    /// AI-generated analytics
    /// </summary>
    public AiAnalyticsDto? AiAnalytics { get; set; }

    /// <summary>
    /// Indicates if this is an enquiry/complaint
    /// </summary>
    public bool IsEnquiry { get; set; }

    /// <summary>
    /// Enquiry/complaint tracking details
    /// </summary>
    public EnquiryDetailsDto? EnquiryDetails { get; set; }

    /// <summary>
    /// Review author information
    /// </summary>
    public UserDto? User { get; set; }

    /// <summary>
    /// Categorization tags
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Review creation timestamp
    /// </summary>
    public string? Timestamp { get; set; }

    /// <summary>
    /// Cosmos DB timestamp
    /// </summary>
    public long Ts { get; set; }
}
