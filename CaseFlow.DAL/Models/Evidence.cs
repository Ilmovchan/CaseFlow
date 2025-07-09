using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Evidence
{
    public int EvidenceId { get; set; }

    public int CaseId { get; set; }

    public int TypeId { get; set; }

    public DateTime CollectedAt { get; set; }

    public int AddressId { get; set; }

    public string? Description { get; set; }

    public string? Purpose { get; set; }

    public string? FilePath { get; set; }

    public string? Notes { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public int ApprovalStatusId { get; set; }

    public string? ApprovalStatusDescription { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ApprovalStatus ApprovalStatus { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;
}
