using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeansAPI.Models;

[Table("Beans")]
[Serializable]
public class Bean
{
    [Key]
    [Required]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    [Required]
    public int Index { get; set; }
    [Required]
    public float CostGBP { get; set; }
    [Required]
    public required string Image { get; set; }
    [Required]
    public required string Colour { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public required string Country { get; set; }
}

// Used to read from the JSON file, since we don't use IsBOTD in bean table in the database
// and some variable names are different (_id, CostGBP, lowercase colour, index)
public class BeanDTO {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string _id { get; set; }
    public int index { get; set; }
    public bool IsBOTD { get; set; }
    public required string Cost { get; set; }
    public required string Image { get; set; }
    public required string colour { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Country { get; set; }

    public Bean toBean() {
        return new Bean {
            Id = _id,
            Index = index,
            CostGBP = float.Parse(Cost.Replace("£", "").Trim()),
            Image = Image,
            Colour = colour,
            Name = Name,
            Description = Description,
            Country = Country
        };
    }
}