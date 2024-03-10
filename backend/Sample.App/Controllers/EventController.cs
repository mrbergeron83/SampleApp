using Microsoft.AspNetCore.Mvc;
using Sample.Domain.Events;
using Sample.Domain.Models;

namespace Sample.App.Controllers;

[ApiController]
[Route("/events")]
public class EventController : ControllerBase
{
    private readonly CreateEvent _createEvent;

    public EventController(CreateEvent createEvent)
    {
        _createEvent = createEvent;
    }

    [HttpGet(Name = "GetEvents")]
    public IEnumerable<EventModel> Get()
    {
        return Enumerable
            .Range(1, 5)
            .Select(index =>
            {
                return new EventModel()
                {
                    Id = index,
                    Name = $"Name {index}",
                    Description = $"Description {index}",
                    DateFromUtcTicks = new DateTimeOffset().UtcTicks,
                    DateToUtcTicks = new DateTimeOffset().UtcTicks
                };
            })
            .ToArray();
    }

    [HttpPost(Name = "CreateEvent")]
    public async Task<EventModel> Post([FromBody] EventModel eventModel)
    {
        return await _createEvent.Execute(eventModel);
    }
}
