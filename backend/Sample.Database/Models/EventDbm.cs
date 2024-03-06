using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Database.Models;

[Table("tbl_Events", Schema = "Sample")]
public class EventDbm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTimeOffset DateFrom { get; set; }
    public DateTimeOffset DateTo { get; set; }
}
