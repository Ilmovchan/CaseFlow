using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class AdminProfile
{
    public int UserId { get; set; }

    public decimal Experience { get; set; }

    public string? PersonalInfo { get; set; }

    public virtual User User { get; set; } = null!;
}
