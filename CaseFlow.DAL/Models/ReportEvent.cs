using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class ReportEvent
{
    public int ReportEventId { get; set; }

    public int ReportId { get; set; }

    public int EntityId { get; set; }

    public int EntityTypeId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual ReportEntityType EntityType { get; set; } = null!;

    public virtual Report Report { get; set; } = null!;
}
