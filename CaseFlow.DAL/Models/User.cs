using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int EmployeeId { get; set; }

    public int RoleId { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual AdminProfile? AdminProfile { get; set; }

    public virtual DetectiveProfile? DetectiveProfile { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SessionLog> SessionLogs { get; set; } = new List<SessionLog>();
}
