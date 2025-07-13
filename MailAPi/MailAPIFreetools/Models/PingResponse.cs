using System.Text.Json.Serialization;

namespace MailAPIFreetools.Models;

/// <summary>
/// Response model for the ping endpoint
/// </summary>
public class PingResponse
{
    /// <summary>
    /// Status of the API service
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
} 