using Sample.Shared.Dtos;
using Sample.Shared.Queriers;

namespace Sample.Domain.Queriers;

public class EventQuerier : IEventQuerier
{
    public IEnumerable<EventDto> GetEvents()
    {
        throw new NotImplementedException();
    }
}
