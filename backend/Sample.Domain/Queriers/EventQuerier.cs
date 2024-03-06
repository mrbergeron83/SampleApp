using Sample.Shared.Dtos;
using Sample.Shared.Queriers;

namespace Sample.Domain.Queriers;

public class EventQuerier : IEventQuerier
{
    public IEnumerable<EventModel> GetEvents()
    {
        throw new NotImplementedException();
    }
}
