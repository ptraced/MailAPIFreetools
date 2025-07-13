using MailAPIFreetools;

Console.WriteLine("MailAPI Freetools - .NET Client Example");
Console.WriteLine("======================================");

// Note: Replace with your actual API key
string apiKey = "your-api-key-here";

if (apiKey == "your-api-key-here")
{
    Console.WriteLine("⚠️  Please set your API key in the Program.cs file");
    Console.WriteLine("   You can get an API key from: https://mailapi.freetools.fr");
    return;
}

try
{
    // Initialize the client
    using var client = new TempMailClient(apiKey);
    
    // Test API connectivity
    Console.WriteLine("🔍 Testing API connectivity...");
    var pingResponse = await client.PingAsync();
    Console.WriteLine($"✅ API Status: {pingResponse.Status}");
    Console.WriteLine();
    
    // Generate a temporary email
    Console.WriteLine("📧 Generating temporary email...");
    var emailResponse = await client.GenerateEmailAsync();
    Console.WriteLine($"✅ Generated email: {emailResponse.GenerateEmail}");
    Console.WriteLine($"📊 Email uptime: {emailResponse.Uptime} days");
    Console.WriteLine($"🔗 Mails endpoint: {emailResponse.MailsEndpoint}");
    Console.WriteLine();
    Console.ReadLine();
    // Fetch emails for the generated address
    Console.WriteLine("📬 Fetching emails...");
    var emailsResponse = await client.GetEmailsAsync(emailResponse.GenerateEmail);
    Console.WriteLine($"✅ Found {emailsResponse.Messages.Count} emails for {emailsResponse.Email}");
    
    if (emailsResponse.Messages.Count > 0)
    {
        Console.WriteLine("\n📨 Email Messages:");
        Console.WriteLine("==================");
        
        foreach (var message in emailsResponse.Messages)
        {
            Console.WriteLine($"📤 From: {message.SenderEmail}");
            Console.WriteLine($"📝 Subject: {message.Subject}");
            Console.WriteLine($"📅 Received: {message.ReceivedAt}");
            Console.WriteLine($"📅 Created: {message.CreatedAt}");
            Console.WriteLine($"💬 Message: {message.Message}");
            
            if (message.Links.Count > 0)
            {
                Console.WriteLine($"🔗 Links: {string.Join(", ", message.Links)}");
            }
            
            Console.WriteLine(new string('-', 50));
        }
    }
    else
    {
        Console.WriteLine("📭 No emails found. Try sending an email to the generated address!");
        Console.WriteLine($"   Send an email to: {emailResponse.GenerateEmail}");
        Console.WriteLine("   Then run this program again to see the received emails.");
    }
    
    Console.WriteLine("\n🎉 Example completed successfully!");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"❌ API request failed: {ex.Message}");
    Console.WriteLine("   Please check your API key and internet connection.");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ An error occurred: {ex.Message}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
