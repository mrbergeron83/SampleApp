using Sample.Domain.Models;

namespace Sample.Domain.Queriers;

public interface IEventQuerier
{
    public Task<IEnumerable<EventModel>> GetEvents();
}
