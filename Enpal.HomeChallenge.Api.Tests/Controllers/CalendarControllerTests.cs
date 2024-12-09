using System.Net;
using System.Net.Http.Json;
using Enpal.HomeChallenge.Api.Tests.Helpers;
using Xunit;
using Enpal.HomeChallenge.Contracts.Requests.Calendar;
using Enpal.HomeChallenge.Contracts.ViewModels.Calendar;
using FluentAssertions;

namespace Enpal.HomeChallenge.Api.Tests.Controllers;

public class CalendarControllerTests : IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public CalendarControllerTests(TestApplicationFactory factory)
    {
        _httpClient = factory.GetClient();
    }

    [Fact]
    public async Task GetAvailableSlots_ShouldReturnAvailableSlots_WhenValidationPassed()
    {
        // Arrange
        var request = new GetAvailableSlotsRequest
        {
            Date = DateOnly.Parse("2024-05-03"),
            Products = new[] { "SolarPanels", "Heatpumps" },
            Language = "German",
            Rating = "Gold"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/calendar/query", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await ApiResponseHelper.GetBodyAsync<IEnumerable<CalendarSlotViewModel>>(
            response,
            CancellationToken.None
        );

        responseBody!.Count().Should().Be(3);
    }

    [Theory]
    [InlineData("2024-05-03", new[] { "Not Panels" }, "German", "Gold")]
    [InlineData("2024-05-03", new[] { "SolarPanels" }, "NotGerman", "Gold")]
    [InlineData("2024-05-03", new[] { "SolarPanels" }, "German", "NotGold")]
    public async Task GetAvailableSlots_ShouldReturnBadRequest_WhenValidationFailed(
        string date,
        string[] products,
        string language,
        string rating)
    {
        // Arrange
        var request = new GetAvailableSlotsRequest
        {
            Date = DateOnly.Parse(date),
            Products = products,
            Language = language,
            Rating = rating
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/calendar/query", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}