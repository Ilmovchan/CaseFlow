using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public int CaseId { get; set; }

    public string Type { get; set; } = null!;

    public int Amount { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public int ApprovalStatusId { get; set; }

    public string? ApprovalStatusDescription { get; set; }

    public virtual ApprovalStatus ApprovalStatus { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;
}
