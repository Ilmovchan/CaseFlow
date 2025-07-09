using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class ReportEntityType
{
    public int EntityTypeId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ReportEvent> ReportEvents { get; set; } = new List<ReportEvent>();
}
