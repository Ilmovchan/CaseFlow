using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.DAL.Models;

[Table("expense")]
public class Expense
{
    [Column("id")]
    public int Id { get; set; }

    [Column("case_id")]
    public int CaseId { get; set; }

    [Column("date_time")] 
    public DateTime DateTime { get; set; }

    [Column("purpose")]
    [MaxLength(255)]
    public string Purpose { get; set; } = null!;

    [Column("amount")]
    [Precision(10, 2)]
    public decimal Amount { get; set; }

    [Column("annotation")]
    public string? Annotation { get; set; }

    public Case Case { get; set; } = null!;
}