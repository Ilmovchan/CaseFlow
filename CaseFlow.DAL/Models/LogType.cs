using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class LogType
{
    public int TypeId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
}
