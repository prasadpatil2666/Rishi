namespace StayFocusAPI.DTOs;

/// <summary>
/// Enquiry or complaint tracking and resolution information
/// </summary>
public class EnquiryDetailsDto
{
    /// <summary>
    /// Unique enquiry identifier
    /// </summary>
    public string? EnquiryId { get; set; }

    /// <summary>
    /// Current status of the enquiry
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Priority level (High, Medium, Low)
    /// </summary>
    public string? Priority { get; set; }

    /// <summary>
    /// ID of the assigned support agent
    /// </summary>
    public string? AssignedAgent { get; set; }

    /// <summary>
    /// Last update timestamp (ISO 8601 format)
    /// </summary>
    public string? LastUpdate { get; set; }

    /// <summary>
    /// SLA deadline (ISO 8601 format)
    /// </summary>
    public string? SlaDeadline { get; set; }
}
