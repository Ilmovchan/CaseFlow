using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Suspect
{
    public int SuspectId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FatherName { get; set; }

    public string? Nickname { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? AddressId { get; set; }

    public string? Notes { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public string? PhysicalDescription { get; set; }

    public string? Alibi { get; set; }

    public bool? IsPriorConvicted { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public int ApprovalStatusId { get; set; }

    public string? ApprovalStatusDescription { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ApprovalStatus ApprovalStatus { get; set; } = null!;

    public virtual ICollection<CaseSuspect> CaseSuspects { get; set; } = new List<CaseSuspect>();
}
