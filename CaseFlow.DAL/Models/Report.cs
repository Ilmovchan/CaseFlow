using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFlow.DAL.Models;

[Table("report")]
public class Report
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("case_id")]
    public int CaseId { get; set; }

    [Column("report_date")]
    public DateTime ReportDate { get; set; } = DateTime.Now; 

    [Column("summary")]
    public string Summary { get; set; } = null!;

    [Column("comments")]
    public string? Comments { get; set; }

    public Case Case { get; set; } = null!;
}