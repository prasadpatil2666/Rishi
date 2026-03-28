namespace StayFocusAPI.Configuration;

/// <summary>
/// Configuration settings for Azure Cosmos DB connection
/// </summary>
public class CosmosDbSettings
{
    /// <summary>
    /// Gets or sets the Cosmos DB account endpoint URL
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Cosmos DB account key (primary key)
    /// </summary>
    public string AccountKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the database name
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the container name
    /// </summary>
    public string ContainerName { get; set; } = string.Empty;
}
