using Microsoft.AspNetCore.Mvc;
using Sample.Domain.Events;
using Sample.Domain.Models;
using Sample.Domain.Repositories;

namespace Sample.App.Controllers;

[ApiController]
[Route("/events")]
public class EventController : ControllerBase
{
    private readonly CreateEvent _createEvent;
    private readonly EventRepository _eventRepository;

    public EventController(CreateEvent createEvent, EventRepository eventRepository)
    {
        _createEvent = createEvent;
        _eventRepository = eventRepository;
    }

    [HttpGet(Name = "GetEvents")]
    public async Task<IEnumerable<EventModel>> Get()
    {
        return await this._eventRepository.GetEvents();
    }

    [HttpPost(Name = "CreateEvent")]
    public async Task<EventModel> Post([FromBody] EventModel eventModel)
    {
        return await _createEvent.Execute(eventModel);
    }
}
