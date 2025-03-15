using Microsoft.AspNetCore.HttpsPolicy;

var builder = WebApplication.CreateBuilder(args);

// Apply default configurations from the library
builder.AddServiceDefaults();

// Add controllers to the services
builder.Services.AddControllers();

// Configure HTTPS redirection using a configurable port
builder.Services.Configure<HttpsRedirectionOptions>(options =>
{
    // Read the HTTPS port from configuration (e.g., appsettings.json or environment variables)
    string? httpsPortStr = builder.Configuration["HttpsPort"] ?? Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT");
    if (int.TryParse(httpsPortStr, out int httpsPort))
    {
        options.HttpsPort = httpsPort; // Set the port dynamically
    }
    else
    {
        throw new InvalidOperationException("HTTPS port is not configured. Please define 'HttpsPort' in appsettings.json or set the 'ASPNETCORE_HTTPS_PORT' environment variable.");
    }
});

var app = builder.Build();

app.UseHttpsRedirection(); // Enable HTTPS redirection
app.UseAuthorization(); // Enable authorization middleware
app.MapControllers(); // Map controller routes

// Map health check endpoints from the library
app.MapDefaultEndpoints();

await app.RunAsync();
