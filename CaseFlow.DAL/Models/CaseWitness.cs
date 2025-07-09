using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class CaseWitness
{
    public int CaseWitnessId { get; set; }

    public int CaseId { get; set; }

    public int WitnessId { get; set; }

    public string? Notes { get; set; }

    public string? ApprovalStatusDescription { get; set; }

    public int ApprovalStatusId { get; set; }

    public virtual ApprovalStatus ApprovalStatus { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual ICollection<Testimony> Testimonies { get; set; } = new List<Testimony>();

    public virtual Witness Witness { get; set; } = null!;
}
