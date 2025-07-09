using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Testimony
{
    public int TestimonyId { get; set; }

    public int CaseWitnessId { get; set; }

    public DateTime CollectedAt { get; set; }

    public string? Content { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual CaseWitness CaseWitness { get; set; } = null!;
}
