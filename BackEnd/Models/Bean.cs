namespace BeansAPI.Models;

public class Bean
{
    public Guid Id { get; set; }
    public int Index { get; set; }
    public bool IsBOTD { get; set; }
    public float CostGBP { get; set; }
    public required string ImageUrl { get; set; }
    public required string Colour { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Country { get; set; }
}