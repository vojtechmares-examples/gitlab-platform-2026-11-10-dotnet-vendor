using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotnetVendor.Tests;

public class EndpointTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Health_is_ok()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Version_names_the_application()
    {
        var body = await _client.GetFromJsonAsync<Dictionary<string, string>>("/api/version");

        Assert.Equal("dotnet-vendor", body!["application"]);
    }

    [Fact]
    public async Task Home_page_is_html()
    {
        var response = await _client.GetAsync("/");

        Assert.Equal("text/html", response.Content.Headers.ContentType?.MediaType);
        Assert.Contains("dotnet-vendor", await response.Content.ReadAsStringAsync());
    }
}
