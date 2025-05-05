using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Evidence
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateOnly CollectionDate { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
