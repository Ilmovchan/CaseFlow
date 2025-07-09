using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class SessionLog
{
    public int SessionId { get; set; }

    public int UserId { get; set; }

    public DateTime LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public int LoginStatusId { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual LogStatus LoginStatus { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
