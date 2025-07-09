using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class ApprovalStatus
{
    public int StatusId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<CaseWitness> CaseWitnesses { get; set; } = new List<CaseWitness>();

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual ICollection<Evidence> Evidences { get; set; } = new List<Evidence>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Suspect> Suspects { get; set; } = new List<Suspect>();
}
