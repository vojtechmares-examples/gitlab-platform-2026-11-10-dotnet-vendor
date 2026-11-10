var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var version = app.Configuration["APP_VERSION"] ?? "dev";

app.MapGet("/", () => Results.Content(
    $"""
    <!doctype html><html lang="en"><meta charset="utf-8"><title>dotnet-vendor</title>
    <body style="font:18px system-ui;max-width:40rem;margin:3rem auto">
    <h1>dotnet-vendor</h1>
    <p>Delivered by the vendor, version <b>{version}</b>, environment <b>{app.Environment.EnvironmentName}</b>.</p>
    <p><a href="/api/version">/api/version</a> &middot; <a href="/health">/health</a></p></body></html>
    """, "text/html"));

app.MapGet("/api/version", () => new
{
    application = "dotnet-vendor",
    version,
    environment = app.Environment.EnvironmentName,
    host = Environment.MachineName,
    runtime = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription,
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();

// Lets the integration tests reach the entry point
public partial class Program;
