using System.Text.Json.Serialization;

namespace MailAPIFreetools.Models;

/// <summary>
/// Response model for the emails endpoint
/// </summary>
public class EmailsResponse
{
    /// <summary>
    /// Status of the request
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The email address that was queried
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// List of messages received by the email address
    /// </summary>
    [JsonPropertyName("messages")]
    public List<EmailMessage> Messages { get; set; } = new();
}

/// <summary>
/// Individual email message model
/// </summary>
public class EmailMessage
{
    /// <summary>
    /// Email address of the sender
    /// </summary>
    [JsonPropertyName("sender_email")]
    public string SenderEmail { get; set; } = string.Empty;

    /// <summary>
    /// Subject of the email
    /// </summary>
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// When the email was received (UTC)
    /// </summary>
    [JsonPropertyName("received_at")]
    public string ReceivedAt { get; set; } = string.Empty;

    /// <summary>
    /// When the email was created (UTC)
    /// </summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = string.Empty;

    /// <summary>
    /// The email content/body
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Links found in the email
    /// </summary>
    [JsonPropertyName("links")]
    public List<string> Links { get; set; } = new();
} 