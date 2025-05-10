using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeansAPI.Models;

[Table("Beans")]
[Serializable]
public class Bean
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public int Index { get; set; }
    [Required]
    public float CostGBP { get; set; }
    [Required]
    public required string ImageUrl { get; set; }
    [Required]
    public required string Colour { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public required string Country { get; set; }
}