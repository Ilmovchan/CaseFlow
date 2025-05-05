using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Expense
{
    public int Id { get; set; }

    public int CaseId { get; set; }

    public DateTime DateTime { get; set; }

    public string Purpose { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Annotation { get; set; }

    public virtual Case Case { get; set; } = null!;
}
