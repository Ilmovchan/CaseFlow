using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class SuspectStatus
{
    public int StatusId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<CaseSuspect> CaseSuspects { get; set; } = new List<CaseSuspect>();
}
