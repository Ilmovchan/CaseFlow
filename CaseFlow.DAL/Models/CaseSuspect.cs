using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class CaseSuspect
{
    public int SuspectId { get; set; }

    public int CaseId { get; set; }

    public bool IsInterrogated { get; set; }

    public string? Alibi { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual Suspect Suspect { get; set; } = null!;
}
