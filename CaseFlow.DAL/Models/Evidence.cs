using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFlow.DAL.Models;

[Table("evidence")]
public class Evidence
{
    [Column("id")]
    public int Id { get; set; }

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("type")]
    [MaxLength(100)]
    public string Type { get; set; } = null!;

    [Column("collection_date")]
    public DateTime CollectionDate { get; set; }

    public ICollection<Case> Cases { get; set; } = new List<Case>();
}