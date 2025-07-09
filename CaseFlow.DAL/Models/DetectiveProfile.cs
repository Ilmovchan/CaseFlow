using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class DetectiveProfile
{
    public int UserId { get; set; }

    public decimal Experience { get; set; }

    public string? PersonalInfo { get; set; }

    public string Rank { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
