using Microsoft.AspNetCore.Mvc;
using Sample.Shared.Dtos;

namespace Sample.App.Controllers;

[ApiController]
[Route("/events")]
public class EventController : ControllerBase
{
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
}
