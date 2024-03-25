using Microsoft.EntityFrameworkCore;
using Sample.Database;
using Sample.Domain.Models;

namespace Sample.Domain.Queriers;

public class EventQuerier : IEventQuerier
{
    private readonly SampleDbContext _db;

    public EventQuerier(SampleDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<EventModel>> GetEvents()
    {
        return await this._db.Events
            .Select(x => new EventModel()
            { 
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DateFromUnixSeconds = x.DateFrom.ToUnixTimeSeconds(),
                DateToUnixSeconds = x.DateTo.ToUnixTimeSeconds() 
            })
            .ToListAsync();
    }
}
