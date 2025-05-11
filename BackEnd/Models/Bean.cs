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
    [SQLite.Unique]
    [Required]
    public required int Index { get; set; }
    [Required]
    public required float CostGBP { get; set; }
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

    public BeanApiDTO ToBeanApiDTO() {
        return new BeanApiDTO
        {
            Index = Index,
            CostGBP = CostGBP,
            Image = Image,
            Colour = Colour,
            Name = Name,
            Description = Description,
            Country = Country
        };
    }
}

// Used to read from the JSON file, since we don't use IsBOTD in bean table in the database
// and some variable names are different (_id, CostGBP, lowercase colour, index)
public class BeanJsonDTO {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string _id { get; set; }
    public required int index { get; set; }
    public required bool IsBOTD { get; set; }
    public required string Cost { get; set; }
    public required string Image { get; set; }
    public required string colour { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Country { get; set; }

    public Bean ToBean() {
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

public class BeanApiDTO
{
    public int? Index { get; set; }
    public required float CostGBP { get; set; }
    public required string Image { get; set; }
    public required string Colour { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Country { get; set; }

    public Bean CreateBean(int? index) {
        if (index == null && Index == null)
        {
            throw new ArgumentException("Index must be provided for entry creation.");
        }
        return new Bean
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Index = index ?? Index ?? -1,
            CostGBP = CostGBP,
            Image = Image,
            Colour = Colour,
            Name = Name,
            Description = Description,
            Country = Country
        };
    }
}