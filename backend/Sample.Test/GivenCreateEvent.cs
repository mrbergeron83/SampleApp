using FluentAssertions;
using Newtonsoft.Json;
using Sample.Domain.Models;
using System.Text;

namespace Sample.Test;

public class GivenCreateEvent : SampleTestBase
{
    private readonly HttpClient _httpClient;
    public GivenCreateEvent()
    {
        _httpClient = CreateClient();
        
    }
    [Fact]
    public async Task When_Creating_Then_Creates()
    {
        // Arrange
        var eventModel = new EventModel()
        {
            Name = "Test",
            Description = "Test",
            DateFromUtcTicks = DateTimeOffset.UtcNow.AddDays(-65).ToUnixTimeSeconds(),
            DateToUtcTicks = DateTimeOffset.UtcNow.AddDays(65).ToUnixTimeSeconds()
        };
        var payload = JsonConvert.SerializeObject(eventModel);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/events", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var reponse = JsonConvert.DeserializeObject<EventModel>(await response.Content.ReadAsStringAsync());

        var expectedEventModel = eventModel;
        expectedEventModel.Id = 1;

        reponse.Should().BeEquivalentTo(expectedEventModel);
    }

    [Fact]
    public async Task When_Creating_with_bad_object_Then_throws()
    {
        // Arrange
        var payload = JsonConvert.SerializeObject(new { });
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/events", content);

        // Assert
        var action = () => response.EnsureSuccessStatusCode();

        action.Should().Throw<HttpRequestException>();
    }

    [Fact]
    public async Task When_Creating_with_id_generated_Then_returns_error()
    {
        // Arrange
        var eventModel = new EventModel()
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            DateFromUtcTicks = DateTimeOffset.UtcNow.AddDays(-65).ToUnixTimeSeconds(),
            DateToUtcTicks = DateTimeOffset.UtcNow.AddDays(65).ToUnixTimeSeconds()
        };
        var payload = JsonConvert.SerializeObject(eventModel);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/events", content);

        // Assert
        var action = () => response.EnsureSuccessStatusCode();

        action.Should().Throw<HttpRequestException>();
    }
}
