using System.Net.Http.Headers;
using System.Text.Json;
using MailAPIFreetools.Models;

namespace MailAPIFreetools;

/// <summary>
/// Client for the MailAPI Freetools temporary email service
/// </summary>
public class TempMailClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly JsonSerializerOptions _jsonOptions;
    private bool _disposed;

    /// <summary>
    /// Base URL for the MailAPI Freetools service
    /// </summary>
    public const string BaseUrl = "https://mailapi.freetools.fr";

    /// <summary>
    /// Initialize a new instance of TempMailClient
    /// </summary>
    /// <param name="apiKey">Your API key for the MailAPI Freetools service</param>
    /// <param name="httpClient">Optional HttpClient instance. If not provided, a new one will be created.</param>
    public TempMailClient(string apiKey, HttpClient? httpClient = null)
    {
        if (string.IsNullOrEmpty(apiKey))
            throw new ArgumentException("API key cannot be null or empty", nameof(apiKey));

        _apiKey = apiKey;
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("MailAPI-Key", _apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    /// <summary>
    /// Generate a fresh temporary email address
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Email generation response containing the temporary email address and metadata</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails</exception>
    /// <exception cref="JsonException">Thrown when the response cannot be deserialized</exception>
    public async Task<EmailGenerationResponse> GenerateEmailAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("/get_email", cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<EmailGenerationResponse>(content, _jsonOptions)
            ?? throw new JsonException("Failed to deserialize email generation response");
    }

    /// <summary>
    /// Fetch all emails received by the specified temporary email address
    /// </summary>
    /// <param name="email">The temporary email address to fetch emails for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Emails response containing all received messages</returns>
    /// <exception cref="ArgumentException">Thrown when email parameter is null or empty</exception>
    /// <exception cref="HttpRequestException">Thrown when the API request fails</exception>
    /// <exception cref="JsonException">Thrown when the response cannot be deserialized</exception>
    public async Task<EmailsResponse> GetEmailsAsync(string email, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        var response = await _httpClient.GetAsync($"/get_mails?mail={Uri.EscapeDataString(email)}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<EmailsResponse>(content, _jsonOptions)
            ?? throw new JsonException("Failed to deserialize emails response");
    }

    /// <summary>
    /// Perform a health check on the API service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ping response indicating the API service status</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails</exception>
    /// <exception cref="JsonException">Thrown when the response cannot be deserialized</exception>
    public async Task<PingResponse> PingAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("/ping", cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<PingResponse>(content, _jsonOptions)
            ?? throw new JsonException("Failed to deserialize ping response");
    }

    /// <summary>
    /// Dispose of the HTTP client and other resources
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _httpClient?.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
} 
