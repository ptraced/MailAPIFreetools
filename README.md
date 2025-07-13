# MailAPI Freetools - .NET Client

A .NET 8 client library for the [MailAPI Freetools](https://mailapi.freetools.fr) temporary email service. Generate temporary email addresses and read emails programmatically.

## Features

- ✅ Generate temporary email addresses
- ✅ Fetch emails from temporary addresses
- ✅ Health check endpoint
- ✅ Fully async/await support
- ✅ Comprehensive XML documentation
- ✅ Built-in error handling
- ✅ Supports dependency injection

## Installation

Install the package via NuGet Package Manager:

```
Install-Package MailAPIFreetools
```

Or via .NET CLI:

```
dotnet add package MailAPIFreetools
```

## Usage

### Basic Usage

```csharp
using MailAPIFreetools;

// Initialize the client with your API key
var client = new TempMailClient("your-api-key-here");

// Generate a temporary email address
var emailResponse = await client.GenerateEmailAsync();
Console.WriteLine($"Generated email: {emailResponse.GenerateEmail}");

// Fetch emails for the generated address
var emailsResponse = await client.GetEmailsAsync(emailResponse.GenerateEmail);
Console.WriteLine($"Received {emailsResponse.Messages.Count} emails");

// Display emails
foreach (var message in emailsResponse.Messages)
{
    Console.WriteLine($"From: {message.SenderEmail}");
    Console.WriteLine($"Subject: {message.Subject}");
    Console.WriteLine($"Received: {message.ReceivedAt}");
    Console.WriteLine($"Message: {message.Message}");
    Console.WriteLine($"Links: {string.Join(", ", message.Links)}");
    Console.WriteLine("---");
}

// Health check
var pingResponse = await client.PingAsync();
Console.WriteLine($"API Status: {pingResponse.Status}");

// Don't forget to dispose
client.Dispose();
```

### Using with Dependency Injection

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MailAPIFreetools;

var builder = Host.CreateApplicationBuilder(args);

// Register HttpClient and TempMailClient
builder.Services.AddHttpClient<TempMailClient>();
builder.Services.AddScoped<TempMailClient>(provider =>
{
    var httpClient = provider.GetRequiredService<HttpClient>();
    return new TempMailClient("your-api-key-here", httpClient);
});

var app = builder.Build();

// Use the client
using var scope = app.Services.CreateScope();
var client = scope.ServiceProvider.GetRequiredService<TempMailClient>();

var emailResponse = await client.GenerateEmailAsync();
Console.WriteLine($"Generated email: {emailResponse.GenerateEmail}");
```

### Error Handling

The client throws the following exceptions:

- `ArgumentException`: When required parameters are null or empty
- `HttpRequestException`: When API requests fail
- `JsonException`: When response deserialization fails

```csharp
try
{
    var client = new TempMailClient("your-api-key-here");
    var emailResponse = await client.GenerateEmailAsync();
    // Use the response...
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"API request failed: {ex.Message}");
}
catch (JsonException ex)
{
    Console.WriteLine($"Failed to parse response: {ex.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid parameter: {ex.Message}");
}
```

## API Reference

### TempMailClient

The main client class for interacting with the MailAPI Freetools service.

#### Constructor

```csharp
TempMailClient(string apiKey, HttpClient? httpClient = null)
```

- `apiKey`: Your API key for the MailAPI Freetools service
- `httpClient`: Optional HttpClient instance. If not provided, a new one will be created.

#### Methods

##### GenerateEmailAsync

```csharp
Task<EmailGenerationResponse> GenerateEmailAsync(CancellationToken cancellationToken = default)
```

Generate a fresh temporary email address.

##### GetEmailsAsync

```csharp
Task<EmailsResponse> GetEmailsAsync(string email, CancellationToken cancellationToken = default)
```

Fetch all emails received by the specified temporary email address.

##### PingAsync

```csharp
Task<PingResponse> PingAsync(CancellationToken cancellationToken = default)
```

Perform a health check on the API service.

## Models

### EmailGenerationResponse

Response from the email generation endpoint.

- `Status`: Status of the request
- `GenerateEmail`: The generated temporary email address
- `MailsEndpoint`: Endpoint URL for fetching emails
- `Uptime`: Email uptime in days
- `Contact`: Contact information

### EmailsResponse

Response from the emails endpoint.

- `Status`: Status of the request
- `Email`: The queried email address
- `Messages`: List of received messages

### EmailMessage

Individual email message.

- `SenderEmail`: Sender's email address
- `Subject`: Email subject
- `ReceivedAt`: When the email was received (UTC)
- `CreatedAt`: When the email was created (UTC)
- `Message`: Email content/body
- `Links`: Links found in the email

### PingResponse

Response from the ping endpoint.

- `Status`: API service status

## Requirements

- .NET 8.0 or later
- Valid API key from [MailAPI Freetools](https://mailapi.freetools.fr)

## License

MIT License

## Support

For support, please contact the MailAPI Freetools team at their Telegram: @deleteduser864 