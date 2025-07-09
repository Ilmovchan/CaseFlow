using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Case
{
    public int CaseId { get; set; }

    public string ReferenceNumber { get; set; } = null!;

    public string? Subtitle { get; set; }

    public int ClientId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? AddressId { get; set; }

    public int TypeId { get; set; }

    public int PriorityId { get; set; }

    public int StatusId { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public string? Description { get; set; }

    public int ApprovalStatusId { get; set; }

    public string? ApprovalStatusDescription { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ApprovalStatus ApprovalStatus { get; set; } = null!;

    public virtual ICollection<CaseSuspect> CaseSuspects { get; set; } = new List<CaseSuspect>();

    public virtual CaseWitness? CaseWitness { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Evidence> Evidences { get; set; } = new List<Evidence>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual CasePriority Priority { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual CaseStatus Status { get; set; } = null!;

    public virtual CaseType Type { get; set; } = null!;

    public virtual ICollection<DetectiveProfile> Detectives { get; set; } = new List<DetectiveProfile>();
}
