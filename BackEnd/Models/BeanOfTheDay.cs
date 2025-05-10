using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeansAPI.Models;

[Table("BeanOfTheDay")]
[Serializable]
public class BeanOfTheDay
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Bean")]
    public Guid BeanId { get; set; }
}
