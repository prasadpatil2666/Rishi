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
    /// Initializes the database and container references
    /// </summary>
    private async Task InitializeAsync()
    {
        if (_database == null)
        {
            _database = _cosmosClient.GetDatabase(_databaseName);
        }

        if (_container == null)
        {
            _container = _database.GetContainer(_containerName);
        }

        await Task.CompletedTask;
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
