using System.ComponentModel.DataAnnotations;

namespace Sample.Shared.Dtos;

public class EventModel
{
    public int? Id { get; set; }
    [Required]
    [StringLength(35)]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public long DateFromUtcTicks{ get; set; }
    [Required]
    public long DateToUtcTicks { get; set; }
}