using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CaseFlow.DAL.Enums;

namespace CaseFlow.DAL.Models;

[Table("case")]
public class Case
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("case_type_id")]
    public int CaseTypeId { get; set; }

    [Required]
    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("detective_id")]
    public int? DetectiveId { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("title")]
    public string Title { get; set; } = null!;

    [Required]
    [Column("description")]
    public string Description { get; set; } = null!;

    [Required]
    [Column("start_date")]
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required]
    [Column("deadline_date")]
    public DateOnly DeadlineDate { get; set; }

    [Column("close_date")]
    public DateOnly? CloseDate { get; set; }

    [Required]
    [Column("status")]
    public CaseStatus Status { get; set; } = CaseStatus.Відкрито;

    public CaseType? CaseType { get; set; }
    public Client? Client { get; set; }
    public Detective? Detective { get; set; }

    public ICollection<CaseSuspect>? CaseSuspects { get; set; }
    public ICollection<Report>? Reports { get; set; }
    public ICollection<Expense>? Expenses { get; set; }
    public ICollection<Evidence>? Evidences { get; set; }
}