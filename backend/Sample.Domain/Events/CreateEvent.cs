﻿using Sample.Database;
using Sample.Database.Models;
using Sample.Domain.Models;

namespace Sample.Domain.Events;

public class CreateEvent
{
    private readonly SampleDbContext _db;

    public CreateEvent(SampleDbContext db)
    {
        _db = db;
    }

    public async Task<EventModel> Execute(EventModel model)
    {
        if (model.Id.HasValue)
        {
            throw new InvalidOperationException("Cannot create Event with Id, Ids are autogenerated");
        }
        var dbm = new EventDbm()
        {
            Name = model.Name,
            Description = model.Description,
            DateFrom = DateTimeOffset.FromUnixTimeSeconds(model.DateFromUnixSeconds),
            DateTo = DateTimeOffset.FromUnixTimeSeconds(model.DateToUnixSeconds)
        };
        _db.Add(dbm);
        await _db.SaveChangesAsync();
        model.Id = dbm.Id;
        return model;
    }
}
