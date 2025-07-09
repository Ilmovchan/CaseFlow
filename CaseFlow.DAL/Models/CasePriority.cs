using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class CasePriority
{
    public int PriorityId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
