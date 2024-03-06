using Microsoft.AspNetCore.Mvc;
using Sample.Shared.Dtos;

namespace Sample.App.Controllers;

[ApiController]
[Route("/events")]
public class EventController : ControllerBase
{
    [HttpGet(Name = "GetEvents")]
    public IEnumerable<EventDto> Get()
    {
        return Enumerable
            .Range(1, 5)
            .Select(index => new EventDto($"Name {index}", $"Description {index}", new DateTimeOffset().UtcTicks, new DateTimeOffset().UtcTicks))
            .ToArray();
    }
}
