namespace Sample.Shared.Dtos;

public record EventDto(string Name, string Description, long DateFromUtcTicks, long DateToUtcTicks);