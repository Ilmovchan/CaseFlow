using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class ActivityLog
{
    public int LogId { get; set; }

    public int SessionId { get; set; }

    public int UserId { get; set; }

    public DateTime LogTime { get; set; }

    public int LogTypeId { get; set; }

    public int LogStatusId { get; set; }

    public string Details { get; set; } = null!;

    public virtual LogStatus LogStatus { get; set; } = null!;

    public virtual LogType LogType { get; set; } = null!;

    public virtual SessionLog Session { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
