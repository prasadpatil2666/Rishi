using System.Net.Http.Json;

namespace StayFocus.Services;

public class ReviewApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public ReviewApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiBaseUrl = configuration["ApiBaseUrl"] ?? "http://localhost:5000";
    }

    public async Task<List<ReviewDto>?> GetReviewsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ReviewDto>>($"{_apiBaseUrl}/api/reviews");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching reviews: {ex.Message}");
            return null;
        }
    }

    public async Task<ReviewDto?> GetReviewAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ReviewDto>($"{_apiBaseUrl}/api/reviews/{id}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching review: {ex.Message}");
            return null;
        }
    }

    public async Task<ReviewDto?> CreateReviewAsync(ReviewDto review)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/reviews", review);
            return await response.Content.ReadFromJsonAsync<ReviewDto>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating review: {ex.Message}");
            return null;
        }
    }
}

public class ReviewDto
{
    public int Id { get; set; }
    public string Company { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
