using System.Text.Json;

namespace Enpal.HomeChallenge.Api.Tests.Helpers;

public static class ApiResponseHelper
{
    public static async Task<T?> GetBodyAsync<T>(HttpResponseMessage response, CancellationToken ct)
    {
        return await JsonSerializer.DeserializeAsync<T>(
            await response.Content.ReadAsStreamAsync(ct),
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower },
            ct
        );
    }

    public static string? GetContentType(HttpResponseMessage response)
    {
        return response.Content.Headers.ContentType?.ToString();
    }
}