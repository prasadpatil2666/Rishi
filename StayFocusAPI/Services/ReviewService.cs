using Microsoft.Azure.Cosmos;
using StayFocusAPI.DTOs;

namespace StayFocusAPI.Services;

/// <summary>
/// Service for managing review operations in Azure Cosmos DB
/// </summary>
public interface IReviewService
{
    /// <summary>
    /// Retrieves all reviews from Cosmos DB
    /// </summary>
    /// <returns>List of reviews</returns>
    Task<List<CosmosReviewDto>> GetAllReviewsAsync();

    /// <summary>
    /// Retrieves a specific review by ID
    /// </summary>
    /// <param name="reviewId">The review ID</param>
    /// <returns>Review object or null if not found</returns>
    Task<CosmosReviewDto?> GetReviewByIdAsync(string reviewId);
}

/// <summary>
/// Implementation of review service for Cosmos DB operations
/// </summary>
public class ReviewService : IReviewService
{
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;
    private readonly string _containerName;
    private Database? _database;
    private Container? _container;

    public ReviewService(CosmosClient cosmosClient, string databaseName, string containerName)
    {
        _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
        _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>
    /// Initializes the database and container, creating them if they don't exist
    /// </summary>
    private async Task InitializeAsync()
    {
        if (_database == null)
        {
            try
            {
                // Create database if it doesn't exist
                var databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
                _database = databaseResponse.Database;
            }
            catch (CosmosException ex)
            {
                throw new InvalidOperationException($"Failed to initialize database '{_databaseName}': {ex.Message}", ex);
            }
        }

        if (_container == null)
        {
            try
            {
                // First, try to get the container if it already exists
                _container = _database.GetContainer(_containerName);
                await _container.ReadContainerAsync();
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Container doesn't exist, try to create it
                try
                {
                    var containerResponse = await _database.CreateContainerAsync(
                        _containerName,
                        partitionKeyPath: "/id",
                        throughput: 400);
                    _container = containerResponse.Container;
                }
                catch (CosmosException createEx) when (createEx.StatusCode == System.Net.HttpStatusCode.BadRequest 
                    && createEx.Message.Contains("throughput", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        $"Cannot create container '{_containerName}': Your Cosmos DB account has reached its throughput limit. " +
                        $"Please either delete unused containers, increase your throughput limit, or manually create the container in Azure Portal.", 
                        createEx);
                }
                catch (CosmosException createEx)
                {
                    throw new InvalidOperationException($"Failed to create container '{_containerName}': {createEx.Message}", createEx);
                }
            }
            catch (CosmosException ex)
            {
                throw new InvalidOperationException($"Failed to initialize container '{_containerName}': {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Retrieves all reviews from the Cosmos DB container
    /// </summary>
    public async Task<List<CosmosReviewDto>> GetAllReviewsAsync()
    {
        try
        {
            await InitializeAsync();

            if (_container == null)
            {
                throw new InvalidOperationException("Container not initialized");
            }

            var query = "SELECT * FROM c";
            var iterator = _container.GetItemQueryIterator<CosmosReviewDto>(query);

            var reviews = new List<CosmosReviewDto>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                reviews.AddRange(response);
            }

            return reviews;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new InvalidOperationException($"Reviews container not found in Cosmos DB. Please ensure the database '{_databaseName}' and container '{_containerName}' exist.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error retrieving reviews from Cosmos DB: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves a specific review by its ID
    /// </summary>
    public async Task<CosmosReviewDto?> GetReviewByIdAsync(string reviewId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(reviewId))
            {
                throw new ArgumentException("Review ID cannot be null or empty", nameof(reviewId));
            }

            await InitializeAsync();

            if (_container == null)
            {
                throw new InvalidOperationException("Container not initialized");
            }

            var response = await _container.ReadItemAsync<CosmosReviewDto>(reviewId, new PartitionKey(reviewId));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error retrieving review {reviewId} from Cosmos DB: {ex.Message}", ex);
        }
    }
}
