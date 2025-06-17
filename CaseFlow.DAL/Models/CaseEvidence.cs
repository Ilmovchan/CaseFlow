using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Interfaces;

namespace CaseFlow.DAL.Models;

[Table("case_evidence")]
public class CaseEvidence : IWorkflowEntity
{
    [Column("evidence_id")]
    public int EvidenceId { get; set; }
    
    [Column("case_id")]
    public int CaseId { get; set; }

    [Column("approval_status")]
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Чернетка;
    
    public virtual Case Case { get; set; } = null!;
    public virtual Evidence Evidence { get; set; } = null!;
}