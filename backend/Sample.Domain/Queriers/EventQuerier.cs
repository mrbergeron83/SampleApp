using Sample.Domain.Models;

namespace Sample.Domain.Queriers;

public class EventQuerier : IEventQuerier
{
    public Task<IEnumerable<EventModel>> GetEvents()
    {
        throw new NotImplementedException();
    }
}
