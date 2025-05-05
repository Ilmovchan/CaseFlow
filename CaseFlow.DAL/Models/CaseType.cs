using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class CaseType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
