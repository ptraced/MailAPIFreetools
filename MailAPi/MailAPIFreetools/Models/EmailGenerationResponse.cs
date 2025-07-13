using System.Text.Json.Serialization;

namespace MailAPIFreetools.Models;

/// <summary>
/// Response model for the email generation endpoint
/// </summary>
public class EmailGenerationResponse
{
    /// <summary>
    /// Status of the request
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The generated temporary email address
    /// </summary>
    [JsonPropertyName("generate_email")]
    public string GenerateEmail { get; set; } = string.Empty;

    /// <summary>
    /// Endpoint URL for fetching emails for this address
    /// </summary>
    [JsonPropertyName("mails_endpoint")]
    public string MailsEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Email uptime in days
    /// </summary>
    [JsonPropertyName("uptime")]
    public string Uptime { get; set; } = string.Empty;

    /// <summary>
    /// Contact information
    /// </summary>
    [JsonPropertyName("contact")]
    public ContactInfo Contact { get; set; } = new();
}

/// <summary>
/// Contact information model
/// </summary>
public class ContactInfo
{
    /// <summary>
    /// Telegram contact
    /// </summary>
    [JsonPropertyName("telegram")]
    public string Telegram { get; set; } = string.Empty;
} 