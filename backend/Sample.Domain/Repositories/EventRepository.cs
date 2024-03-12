using Microsoft.EntityFrameworkCore;
using Sample.Database;
using Sample.Domain.Models;

namespace Sample.Domain.Repositories;

public class EventRepository
{
    private readonly SampleDbContext _context;

    public EventRepository(SampleDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventModel>> GetEvents()
    {
        return await this._context.Events.Select(x => new EventModel
        { 
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            DateFromUnixSeconds = x.DateFrom.ToUnixTimeSeconds(),
            DateToUnixSeconds = x.DateTo.ToUnixTimeSeconds()
        })
        .ToArrayAsync();
    }
}
