using Microsoft.AspNetCore.HttpsPolicy;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Clear default logging providers and add console logging with debug level
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Configure OpenTelemetry for tracing and metrics
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService("OtelDemoApi")) // Add service name to resource
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation() // Add instrumentation for ASP.NET Core
        .AddHttpClientInstrumentation() // Add instrumentation for HttpClient
        .AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("http://otel-collector:4317"); // Set OTLP exporter endpoint for tracing
        }))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation() // Add instrumentation for ASP.NET Core
        .AddHttpClientInstrumentation() // Add instrumentation for HttpClient
        .AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("http://otel-collector:4317"); // Set OTLP exporter endpoint for metrics
        }));

// Configure OpenTelemetry logging
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.AddOtlpExporter(opt =>
    {
        opt.Endpoint = new Uri("http://otel-collector:4317"); // Set OTLP exporter endpoint for logging
    });
});


// Add controllers to the services
builder.Services.AddControllers();

// Configure HTTPS redirection to external port 5001
builder.Services.Configure<HttpsRedirectionOptions>(options =>
{
    options.HttpsPort = 5001; // External port on the host
});

var app = builder.Build();

app.UseHttpsRedirection(); // Enable HTTPS redirection
app.UseAuthorization(); // Enable authorization middleware
app.MapControllers(); // Map controller routes

await app.RunAsync();
