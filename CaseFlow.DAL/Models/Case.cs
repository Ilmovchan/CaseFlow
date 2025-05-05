using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Case
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int? DetectiveId { get; set; }

    public int CaseTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly DeadlineDate { get; set; }

    public DateOnly? CloseDate { get; set; }

    public virtual ICollection<CaseSuspect> CaseSuspects { get; set; } = new List<CaseSuspect>();

    public virtual CaseType CaseType { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Detective? Detective { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Evidence> Evidences { get; set; } = new List<Evidence>();
}
