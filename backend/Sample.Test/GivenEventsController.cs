using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sample.Database;
using Sample.Database.Models;
using Sample.Domain.Models;
using System.Text;

namespace Sample.Test;

public class GivenEventsController : SampleTestBase
{
    private readonly HttpClient _httpClient;
    public GivenEventsController()
    {
        _httpClient = CreateClient();
    }

    [Fact]
    public async Task When_Getting_then_returns_Events()
    {
        using var scope = this.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SampleDbContext>();

        var now = DateTimeOffset.UtcNow;
        EventDbm[] events = [
            new EventDbm() { Name = "test1", Description = "desc1", DateFrom = now.AddDays(-10), DateTo = now.AddDays(10)},
            new EventDbm() { Name = "test2", Description = "desc2", DateFrom = now.AddDays(-10), DateTo = now.AddDays(10)}
            ];
        db.AddRange(events);
        await db.SaveChangesAsync();

        var response = await this._httpClient.GetAsync("/events");
        response.EnsureSuccessStatusCode();
        var results = JsonConvert.DeserializeObject<EventModel[]>(await response.Content.ReadAsStringAsync());

        EventModel[] expectedResults = [
            new EventModel(){
                Id = events[0].Id,
                Name = events[0].Name,
                Description = events[0].Description,
                DateFromUnixSeconds = events[0].DateFrom.ToUnixTimeSeconds(),
                DateToUnixSeconds = events[0].DateTo.ToUnixTimeSeconds()
            },
            new EventModel(){
                Id = events[1].Id,
                Name = events[1].Name,
                Description = events[1].Description,
                DateFromUnixSeconds = events[1].DateFrom.ToUnixTimeSeconds(),
                DateToUnixSeconds = events[1].DateTo.ToUnixTimeSeconds()
            }];
        results.Should().BeEquivalentTo(expectedResults);
    }

    [Fact]
    public async Task When_Creating_Then_Creates()
    {
        // Arrange
        var eventModel = new EventModel()
        {
            Name = "Test",
            Description = "Test",
            DateFromUnixSeconds = DateTimeOffset.UtcNow.AddDays(-65).ToUnixTimeSeconds(),
            DateToUnixSeconds = DateTimeOffset.UtcNow.AddDays(65).ToUnixTimeSeconds()
        };
        var payload = JsonConvert.SerializeObject(eventModel);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/events", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var results = JsonConvert.DeserializeObject<EventModel>(await response.Content.ReadAsStringAsync());

        var expectedEventModel = eventModel;
        expectedEventModel.Id = 1;

        results.Should().BeEquivalentTo(expectedEventModel);
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
            DateFromUnixSeconds = DateTimeOffset.UtcNow.AddDays(-65).ToUnixTimeSeconds(),
            DateToUnixSeconds = DateTimeOffset.UtcNow.AddDays(65).ToUnixTimeSeconds()
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
