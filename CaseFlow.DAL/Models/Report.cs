using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Report
{
    public int Id { get; set; }

    public int CaseId { get; set; }

    public DateOnly? ReportDate { get; set; }

    public string Summary { get; set; } = null!;

    public string? Comments { get; set; }

    public virtual Case Case { get; set; } = null!;
}
