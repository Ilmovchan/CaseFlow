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

    [Column("case_type_id")]
    public int CaseTypeId { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("detective_id")]
    public int? DetectiveId { get; set; }

    [MaxLength(255)]
    [Column("title")]
    public string Title { get; set; } = null!;

    [Column("description")] 
    public string Description { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Column("deadline_date")]
    public DateOnly DeadlineDate { get; set; }

    [Column("close_date")]
    public DateOnly? CloseDate { get; set; }

    [Column("status")]
    public CaseStatus Status { get; set; } = CaseStatus.Opened;

    public CaseType? CaseType { get; set; }
    public Client? Client { get; set; }
    public Detective? Detective { get; set; }

    public ICollection<CaseSuspect>? CaseSuspects { get; set; }
    public ICollection<Report>? Reports { get; set; }
    public ICollection<Expense>? Expenses { get; set; }
    public ICollection<Evidence>? Evidences { get; set; }
}