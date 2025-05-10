using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeansAPI.Models;

[Table("BeanOfTheDay")]
[Serializable]
public class BeanOfTheDay
{
    [Key]
    public required int Id { get; set; }

    [ForeignKey("Bean")]
    [Required]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string BeanId { get; set; }

    [Required]
    public DateTime DateSet { get; set; }
}
