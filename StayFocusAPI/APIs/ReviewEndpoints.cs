using StayFocusAPI.DTOs;
using StayFocusAPI.Services;

namespace StayFocusAPI.APIs;

/// <summary>
/// Extension methods for mapping review API endpoints
/// </summary>
public static class ReviewEndpoints
{
    private static List<ReviewDto> _localReviews = new();

    /// <summary>
    /// Maps all review-related API endpoints
    /// </summary>
    public static void MapReviewEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/reviews")
            .WithName("Reviews")
            .WithOpenApi()
            .WithTags("Reviews");

        // Local in-memory endpoints
        group.MapGet("/", GetLocalReviews)
            .WithName("GetLocalReviews")
            .WithDescription("Get all local in-memory reviews")
            .Produces<List<ReviewDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", GetLocalReview)
            .WithName("GetLocalReview")
            .WithDescription("Get a specific local review by ID")
            .Produces<ReviewDto?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", CreateLocalReview)
            .WithName("CreateLocalReview")
            .WithDescription("Create a new local review")
            .Accepts<ReviewDto>("application/json")
            .Produces<ReviewDto>(StatusCodes.Status201Created);

        // Cosmos DB endpoints
        var cosmosGroup = app.MapGroup("/api/cosmos/reviews")
            .WithName("CosmosReviews")
            .WithOpenApi()
            .WithTags("Cosmos DB Reviews");

        cosmosGroup.MapGet("/", GetCosmosReviews)
            .WithName("GetCosmosReviews")
            .WithDescription("Get all reviews from Azure Cosmos DB")
            .Produces<CosmosResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

        cosmosGroup.MapGet("/{id}", GetCosmosReviewById)
            .WithName("GetCosmosReviewById")
            .WithDescription("Get a specific review from Cosmos DB by ID")
            .Produces<CosmosReviewDto>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);
    }

    // Local review endpoint handlers
    private static IResult GetLocalReviews()
    {
        return Results.Ok(_localReviews);
    }

    private static IResult GetLocalReview(int id)
    {
        var review = _localReviews.FirstOrDefault(r => r.Id == id);
        return review != null ? Results.Ok(review) : Results.NotFound();
    }

    private static IResult CreateLocalReview(ReviewDto review)
    {
        review.Id = _localReviews.Count > 0 ? _localReviews.Max(r => r.Id) + 1 : 1;
        _localReviews.Add(review);
        return Results.Created($"/api/reviews/{review.Id}", review);
    }

    // Cosmos DB endpoint handlers
    private static async Task<IResult> GetCosmosReviews(IReviewService reviewService)
    {
        try
        {
            var reviews = await reviewService.GetAllReviewsAsync();
            return Results.Ok(new CosmosResponse
            {
                Count = reviews.Count,
                Data = reviews
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Error = ex.Message
            });
        }
    }

    private static async Task<IResult> GetCosmosReviewById(string id, IReviewService reviewService)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Results.BadRequest(new ErrorResponse
                {
                    Error = "Review ID cannot be empty"
                });
            }

            var review = await reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return Results.NotFound(new ErrorResponse
                {
                    Error = "Review not found"
                });
            }

            return Results.Ok(review);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Error = ex.Message
            });
        }
    }
}

/// <summary>
/// Response model for Cosmos DB reviews
/// </summary>
public class CosmosResponse
{
    /// <summary>
    /// Total number of reviews returned
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// List of review data
    /// </summary>
    public List<CosmosReviewDto>? Data { get; set; }
}

/// <summary>
/// Error response model
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Error message
    /// </summary>
    public string? Error { get; set; }
}
