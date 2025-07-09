using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class LogStatus
{
    public int StatusId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<SessionLog> SessionLogs { get; set; } = new List<SessionLog>();
}
