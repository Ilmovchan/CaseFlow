using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int CaseId { get; set; }

    public string Title { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public string Conclusions { get; set; } = null!;

    public string? AdditionalInfo { get; set; }

    public DateTime Created { get; set; }

    public int ApprovalStatusId { get; set; }

    public string? ApprovalStatusDescription { get; set; }

    public virtual ApprovalStatus ApprovalStatus { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual ICollection<ReportEvent> ReportEvents { get; set; } = new List<ReportEvent>();
}
