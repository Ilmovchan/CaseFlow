using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class CaseSuspect
{
    public int CaseId { get; set; }

    public int SuspectId { get; set; }

    public int StatusId { get; set; }

    public string? Notes { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual SuspectStatus Status { get; set; } = null!;

    public virtual Suspect Suspect { get; set; } = null!;
}
