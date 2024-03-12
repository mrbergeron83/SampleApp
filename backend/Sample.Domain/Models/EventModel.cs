using System.ComponentModel.DataAnnotations;

namespace Sample.Domain.Models;

public class EventModel
{
    public int? Id { get; set; }
    [Required]
    [StringLength(35)]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public long DateFromUnixSeconds { get; set; }
    [Required]
    public long DateToUnixSeconds { get; set; }
}
