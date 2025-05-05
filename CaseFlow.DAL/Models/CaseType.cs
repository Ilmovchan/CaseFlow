using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.DAL.Models;

[Table("case_type")]
public class CaseType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(100)]
    public string? Name { get; set; }
    
    [Column("price")]
    [Precision(10,2)]
    public decimal Price { get; set; }

    public ICollection<Case>? Cases { get; set; }
}