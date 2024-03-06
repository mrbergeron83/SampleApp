using Sample.Shared.Dtos;

namespace Sample.Shared.Queriers;

public interface IEventQuerier
{
    public IEnumerable<EventDto> GetEvents();
}
