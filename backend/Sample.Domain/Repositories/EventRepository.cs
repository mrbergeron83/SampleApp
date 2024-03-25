using Microsoft.EntityFrameworkCore;
using Sample.Database;
using Sample.Database.Models;
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
        return await _context.Events.Select(x => MapToModel(x))
        .ToArrayAsync();
    }

    private static EventModel MapToModel(EventDbm dbModel)
    {
        return new EventModel
        {
            Id = dbModel.Id,
            Name = dbModel.Name,
            Description = dbModel.Description,
            DateFromUnixSeconds = dbModel.DateFrom.ToUnixTimeSeconds(),
            DateToUnixSeconds = dbModel.DateTo.ToUnixTimeSeconds()
        };
    }
}
